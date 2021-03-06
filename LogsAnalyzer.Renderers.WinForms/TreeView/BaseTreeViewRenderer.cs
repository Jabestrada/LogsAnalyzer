﻿using LogsAnalyzer.Infrastructure.Analysis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace LogsAnalyzer.Renderers.WinForms.TreeView {
    public abstract class BaseTreeViewRenderer {
        public abstract Type AnalyzerType { get;  }
        public abstract TreeNode Render();

        public Dictionary<TreeNode, ContextMenuStrip> ContextMenuStrips = new Dictionary<TreeNode, ContextMenuStrip>();

        public abstract void SetAnalyzer(BaseLogAnalyzer analyzer);

        protected TreeNode CreateNode(string text) {
            return new TreeNode {
                Text = text
            };
        }

        protected TreeNode CreateNodeWithCommonContextMenuStrip(string text) {
            var theNode = new TreeNode {
                Text = text
            };
            ContextMenuStrips.Add(theNode, CreateCommonContextMenuStrip(theNode));
            return theNode;
        }

        protected ToolStripMenuItem CreateCopyToClipboardMenuItem(string menuText, string textToCopy) {
            ToolStripMenuItem menuItem = new ToolStripMenuItem();
            menuItem.Text = menuText;
            menuItem.Click += (source, args) => {
                Clipboard.SetText(textToCopy);
            };
            return menuItem;
        }

        protected IEnumerable<TreeNode> CreateChunkedNodesFromString(string text, int charsPerLine = 100) {
            var chunkDefinitions = StringChunker.ComputeChunksWithMinCharLimit(text, charsPerLine);
            foreach (var chunkDef in chunkDefinitions) {
                var messageChunk = text.Substring(chunkDef.StartPosition, chunkDef.ChunkLength);
                yield return CreateNode(messageChunk);
            }
        }

        protected ToolStripItem CreateOpenFileInNotepadPlusMenuItem(string text, string file) {
            ToolStripItem item = new ToolStripMenuItem();
            item.Text = text;
            item.Click += (sender, e) => {
                Process.Start("notepad++.exe", file);
            };
            return item;
        }

        protected ContextMenuStrip CreateCommonContextMenuStrip(TreeNode node) {
            var contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.Add(CreateCopyToClipboardMenuItem("Copy", node.Text));
            return contextMenuStrip;
        }

    }
}
