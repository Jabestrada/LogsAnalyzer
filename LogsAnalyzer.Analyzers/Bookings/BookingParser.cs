﻿using LogAnalyzer.Analyzers.Bookings.Models;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace LogsAnalyzer.Analyzers.Bookings {
    public class BookingParser {
        public class XmlTokens {
            public const string ROOT_ELEMENT = "UTSv_ProductTypeReservationRQ";
            public const string CLIENT_TRANSACTION_ID = "ClientTransactionId";
            public const string TIMESTAMP = "TimeStamp";
            public const string DISTRIBUTOR = "CompanyShortName";
            public const string CHANNEL_COMMISSION = "ChannelCommission";
            public const string PAYMENT_OPTION = "PaymentOption";
            public const string PRODUCT_ID = "ID";
            public const string START_DATE = "Start";
            public const string END_DATE = "End";
            public const string PRODUCT_TOTAL = "Amount";
            public const string EXTRAS_TOTAL = "TotalRate";
            public const string EXTRA_CODE = "Code";
            public const string EXTRA_NAME = "Name";
            public const string ADULT_PRICE = "AdultPrice";
            public const string PER_ADULT_PRICE = "PerAdultPrice";
            public const string PER_ADULT_PRICE_PER_NIGHT = "PerAdultPricePerNight";
            public const string CHILD_PRICE = "ChildPrice";
            public const string PER_CHILD_PRICE = "PerChildPrice";
            public const string PER_CHILD_PRICE_PER_NIGHT = "PerChildPricePerNight";
            public const string IS_PER_NIGHT = "IsPerNight";
            public const string IS_NIGHTS_SELECTABLE = "IsNightsSelectable";
            public const string IS_ALLOW_OCCUPANCY_SELECT = "IsAllowOccupancySelect";
            public const string SELECTED_NIGHT = "SelectedNight";
            public const string FIXED_PRICE = "FixedPrice";
            public const string FIXED_PRICE_PER_NIGHT = "FixedPricePerNight";

            public const string NS_EVIIVO = "http://www.eviivo.com/UTSv/2004/01/01";
        }

        public BookingAnalysis BookingAnalysis { get; internal set; }

        private StringBuilder _bookingInputBuffer = null;

        private const string NO_VALUE = "<not specified>";

        private BookingAnalysis _lastBookingParsed = null;

        private const string MISC_LOG_PATTERN = @"\[MTD:.+?\](.*)";

        private readonly string SELF_CLOSING_ROOT_ELEMENT_PATTERN = $"<{XmlTokens.ROOT_ELEMENT}[^>]*\\s*/>";

        public bool Accept(string lineText) {
            BookingAnalysis = null;
            BookingAnalysis temp;
            if (tryParseBookingOnSameLine(lineText, out temp)) {
                BookingAnalysis = temp;
                _lastBookingParsed = BookingAnalysis;
            }
            else if (isBookingStartTag(lineText)) {
                _bookingInputBuffer = new StringBuilder();
                _bookingInputBuffer.AppendLine(lineText);
            }
            else if (isBookingEndTag(lineText)) {
                var bookingText = _bookingInputBuffer.AppendLine(lineText).ToString();
                BookingAnalysis = parseBooking(bookingText);
                _lastBookingParsed = BookingAnalysis;
                _bookingInputBuffer = null;
            }
            else {
                if (_bookingInputBuffer != null) {
                    _bookingInputBuffer.AppendLine(lineText);
                }
                else {
                    if (_lastBookingParsed == null) return false;

                    string parsedMiscTraceData;
                    if (tryParseMiscellaneousTraceData(lineText, out parsedMiscTraceData)) {
                        _lastBookingParsed.MiscellaneousTraceData.Add(parsedMiscTraceData);
                        return true;
                    }
                    return false;
                }
            }
            return true;
        }


        private bool tryParseMiscellaneousTraceData(string lineText, out string parsedMiscTraceData) {
            parsedMiscTraceData = lineText;
            var m = Regex.Match(lineText, MISC_LOG_PATTERN);
            if (m.Success && m.Groups.Count > 1) {
                // Check below needed to remove duplicate log entries;
                // much faster than doing via RegEx: .*^(?!.*Rezobx\+EASWebService).*\[MTD:.+?\](.*)
                if (parsedMiscTraceData.Contains(@"Rezobx+EASWebService")) {
                    return false;
                }
                parsedMiscTraceData = $"{m.Groups[1].Value}";
            }
            return m.Success;
        }

        private bool tryParseBookingOnSameLine(string lineText, out BookingAnalysis outputBooking) {
            var m = Regex.Match(lineText, SELF_CLOSING_ROOT_ELEMENT_PATTERN);
            outputBooking = m.Success ? parseBooking(m.Groups[0].Value) : null;
            if (m.Success) return true;

            var openCloseTagSameOnSameLinePattern = $"<{XmlTokens.ROOT_ELEMENT}>.*?</{XmlTokens.ROOT_ELEMENT}>";
            m = Regex.Match(lineText, openCloseTagSameOnSameLinePattern);
            outputBooking = m.Success ? parseBooking(m.Groups[0].Value) : null;
            return m.Success;
        }

        private bool isBookingStartTag(string lineText) {
            return Regex.Match(lineText, $"<{XmlTokens.ROOT_ELEMENT}.*>").Success;
        }

        private bool isBookingEndTag(string lineText) {
            return Regex.Match(lineText, $"</{XmlTokens.ROOT_ELEMENT}>").Success;
        }

        private BookingAnalysis parseBooking(string bookingText) {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(bookingText);

            XmlNamespaceManager nsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
            var evNamespace = "ev";
            nsMgr.AddNamespace(evNamespace, XmlTokens.NS_EVIIVO);
            var evPrefix = evNamespace + ":";

            var booking = new BookingAnalysis();
            parseTransactionIdAndTimestamp(booking, xmlDoc);
            parseProviderAndDistributor(booking, xmlDoc, nsMgr, evPrefix);
            parseProductAndExtras(booking, xmlDoc, nsMgr, evPrefix);
            parseCustomer(booking, xmlDoc, nsMgr, evPrefix);
            return booking;
        }

        private void parseTransactionIdAndTimestamp(BookingAnalysis booking, XmlDocument xmlDoc) {
            booking.TransactionId = xmlDoc.DocumentElement.Attributes[XmlTokens.CLIENT_TRANSACTION_ID]?.Value ?? string.Empty;
            booking.Timestamp = xmlDoc.DocumentElement.Attributes[XmlTokens.TIMESTAMP]?.Value ?? string.Empty;

        }
        private void parseProviderAndDistributor(BookingAnalysis booking, XmlDocument xmlDoc, XmlNamespaceManager nsMgr, string nsPrefix) {
            var firstProviderNode = xmlDoc.SelectSingleNode($"//{nsPrefix}ProviderID_List/{nsPrefix}ID", nsMgr);
            booking.PrimaryProvider = firstProviderNode?.InnerText;
            booking.ProviderCount = xmlDoc.SelectNodes($"//{nsPrefix}ProviderID_List/{nsPrefix}ID", nsMgr)?.Count ?? 0;

            var companyNode = xmlDoc.SelectSingleNode($"//{nsPrefix}POS/{nsPrefix}Source/{nsPrefix}BookingChannel/{nsPrefix}CompanyName", nsMgr);
            booking.DistributorShortName = companyNode?.Attributes[XmlTokens.DISTRIBUTOR]?.Value ?? string.Empty;
        }

        private void parseProductAndExtras(BookingAnalysis booking, XmlDocument xmlDoc, XmlNamespaceManager nsMgr, string nsPrefix) {
            var productTypeNode = xmlDoc.SelectSingleNode($"//{nsPrefix}ProductTypeRsrvs", nsMgr);
            booking.ChannelCommission = productTypeNode?.Attributes[XmlTokens.CHANNEL_COMMISSION]?.Value ?? string.Empty;
            booking.PaymentOption = productTypeNode?.Attributes[XmlTokens.PAYMENT_OPTION]?.Value ?? string.Empty;

            parseProduct(booking, productTypeNode, nsMgr, nsPrefix);
            parseExtras(booking, productTypeNode, nsMgr, nsPrefix);
            parseStartAndEndDates(booking, productTypeNode, nsMgr, nsPrefix);
        }

        private void parseStartAndEndDates(BookingAnalysis booking, XmlNode productTypeNode, XmlNamespaceManager nsMgr, string nsPrefix) {
            var timeSlotNode = productTypeNode?.SelectSingleNode($"descendant::{nsPrefix}TimeSlots/{nsPrefix}TimeSlot", nsMgr);
            booking.StartDate = timeSlotNode?.Attributes[XmlTokens.START_DATE]?.Value ?? string.Empty;
            booking.EndDate = timeSlotNode?.Attributes[XmlTokens.END_DATE]?.Value ?? string.Empty;
        }

        private void parseProduct(BookingAnalysis booking, XmlNode productTypeNode, XmlNamespaceManager nsMgr, string nsPrefix) {
            var productNode = productTypeNode?.SelectSingleNode($"descendant::{nsPrefix}ProductTypeRsrv/{nsPrefix}ProductType", nsMgr);
            booking.ProductId = productNode?.Attributes[XmlTokens.PRODUCT_ID]?.Value ?? string.Empty;
            var productNameNode = productNode?.SelectSingleNode($"descendant::{nsPrefix}Name", nsMgr);
            booking.ProductName = productNameNode?.InnerText ?? string.Empty;

            var productTotalNode = productTypeNode?.SelectSingleNode($"descendant::{nsPrefix}TotalRate", nsMgr);
            booking.ProductTotal = productTotalNode?.Attributes[XmlTokens.PRODUCT_TOTAL]?.Value ?? string.Empty;
        }

        private void parseExtras(BookingAnalysis booking, XmlNode productTypeNode, XmlNamespaceManager nsMgr, string nsPrefix) {
            var extrasMainNode = productTypeNode?.SelectSingleNode($"descendant::{nsPrefix}Supplements", nsMgr);
            booking.ExtrasTotal = extrasMainNode?.Attributes[XmlTokens.EXTRAS_TOTAL]?.Value ?? string.Empty;

            var extraNodes = productTypeNode?.SelectNodes($"descendant::{nsPrefix}Supplement", nsMgr);
            if (extraNodes == null) return;

            foreach (XmlNode extraNode in extraNodes) {
                Extra extra = createExtraFromNode(extraNode);
                parseSelectedNightsOfExtra(extra, extraNode, nsMgr, nsPrefix);
                booking.Extras.Add(extra);
            }

        }

        private Extra createExtraFromNode(XmlNode extraNode) {
            return new Extra {
                Code = extraNode.Attributes[XmlTokens.EXTRA_CODE]?.Value ?? string.Empty,
                Name = extraNode.Attributes[XmlTokens.EXTRA_NAME]?.Value ?? string.Empty,
                FixedPrice = extraNode.Attributes[XmlTokens.FIXED_PRICE]?.Value ?? string.Empty,
                FixedPricePerNight = extraNode.Attributes[XmlTokens.FIXED_PRICE_PER_NIGHT]?.Value ?? string.Empty,
                AdultPrice = extraNode.Attributes[XmlTokens.ADULT_PRICE]?.Value ?? string.Empty,
                PerAdultPrice = extraNode.Attributes[XmlTokens.PER_ADULT_PRICE]?.Value ?? string.Empty,
                PerAdultPricePerNight = extraNode.Attributes[XmlTokens.PER_ADULT_PRICE_PER_NIGHT]?.Value ?? string.Empty,
                ChildPrice = extraNode.Attributes[XmlTokens.CHILD_PRICE]?.Value ?? string.Empty,
                PerChildPrice = extraNode.Attributes[XmlTokens.PER_CHILD_PRICE]?.Value ?? string.Empty,
                PerChildPricePerNight = extraNode.Attributes[XmlTokens.PER_CHILD_PRICE_PER_NIGHT]?.Value ?? string.Empty,
                IsPerNight = extraNode.Attributes[XmlTokens.IS_PER_NIGHT]?.Value ?? NO_VALUE,
                IsNightsSelectable = extraNode.Attributes[XmlTokens.IS_NIGHTS_SELECTABLE]?.Value ?? NO_VALUE,
                IsAllowOccupancySelect = extraNode.Attributes[XmlTokens.IS_ALLOW_OCCUPANCY_SELECT]?.Value ?? NO_VALUE
            };
        }

        private void parseSelectedNightsOfExtra(Extra extra, XmlNode extraNode, XmlNamespaceManager nsMgr, string nsPrefix) {
            var selectedNightNodes = extraNode.SelectNodes($"descendant::{nsPrefix}SelectedNight", nsMgr);
            foreach (XmlNode night in selectedNightNodes) {
                extra.SelectedNights.Add(new Night {
                    SelectedNight = night.Attributes[XmlTokens.SELECTED_NIGHT]?.Value ?? NO_VALUE
                }); ;
            }
        }

        private void parseCustomer(BookingAnalysis booking, XmlDocument xmlDoc, XmlNamespaceManager nsMgr, string nsPrefix) {
            var firstNameNode = xmlDoc.SelectSingleNode($"//{nsPrefix}Customer/{nsPrefix}PersonName/{nsPrefix}GivenName", nsMgr);
            booking.CustomerFirstName = firstNameNode?.InnerText ?? string.Empty;

            var lastNameNode = xmlDoc.SelectSingleNode($"//{nsPrefix}Customer/{nsPrefix}PersonName/{nsPrefix}Surname", nsMgr);
            booking.CustomerLastName = lastNameNode?.InnerText ?? string.Empty;
        }





    }
}
