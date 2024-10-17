using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace LiteFramework.Editor.GUI
{

    public class TableViewItem<T> : TreeViewItem
    {
        public T Data;
    }

    
    public class EditorTableView<T> : TreeView
    {
        private readonly string _sortedColumnIndexStateKey = "EditorTableView_sortedColumnIndex_"+ Guid.NewGuid();
        
        private MultiColumnHeader _multiColumnHeader;
        private readonly List<Column> _columnDefs = new();
        private MultiColumnHeaderState.Column[] _columns;
        private bool _isInitialized;
        private IEnumerable<T> _items;
        
        public delegate void DrawItem(Rect rect, T item);

        public class Column
        {
            internal MultiColumnHeaderState.Column column;
            internal DrawItem onDraw;
            internal Comparison<T> onSort;
            
            public Column SetMaxWidth(float maxWidth)
            {
                column.maxWidth = maxWidth;
                return this;
            }
            
            public Column SetTooltip(string tooltip)
            {
                column.headerContent.tooltip = tooltip;
                return this;
            }

            public Column SetAutoResize(bool autoResize)
            {
                column.autoResize = autoResize;
                return this;
            }

            public Column SetAllowToggleVisibility(bool allow)
            {
                column.allowToggleVisibility = allow;
                return this;
            }
            
            public Column SetSorting(Comparison<T> onSort)
            {
                this.onSort = onSort;
                column.canSort = true;
                return this;
            }
        }
        
        public EditorTableView() : base(new TreeViewState())
        {
            showAlternatingRowBackgrounds = true;
            showBorder = true;
        }

        public EditorTableView(TreeViewState state, MultiColumnHeader header) : base(state, header)
        {
            showAlternatingRowBackgrounds = true;
            showBorder = true;
            header.ResizeToFit();
            header.sortedColumnIndex = SessionState.GetInt(_sortedColumnIndexStateKey, 1);
        }

        public Column AddColumn(string title, int minWidth, DrawItem onDrawItem)
        {
            Column columnDef = new Column()
            {
                column = new MultiColumnHeaderState.Column()
                {
                    allowToggleVisibility = false,
                    autoResize = true,
                    minWidth = minWidth,
                    canSort = false,
                    sortingArrowAlignment = TextAlignment.Right,
                    headerContent = new GUIContent(title),
                    headerTextAlignment = TextAlignment.Left,
                },
                onDraw = onDrawItem
            };
            
            _columnDefs.Add(columnDef);
            return columnDef;
        }
        
        private void ReBuild()
        {
            _columns = _columnDefs.Select((def) => def.column).ToArray();
            multiColumnHeader = new MultiColumnHeader(new MultiColumnHeaderState(_columns));
            multiColumnHeader.visibleColumnsChanged += (multiColumnHeader) => multiColumnHeader.ResizeToFit();
            multiColumnHeader.ResizeToFit();
            Reload();
        }

        protected override TreeViewItem BuildRoot()
        {
            var root = new TreeViewItem { depth = -1 };
            var children = new List<TreeViewItem>();
            for (int i = 0; i < _items.Count(); i++)
            {
                var data = _items.ElementAt(i);
                children.Add(new TableViewItem<T>
                {
                    id = i,
                    Data = data,
                    depth = 0,
                });
                
            }
            root.children = children;
            _isInitialized = true;
            return root;
        }

        public override void OnGUI(Rect rect)
        {
            if (multiColumnHeader == null)
            {
                ReBuild();
            }
            if(!_isInitialized) return;
            base.OnGUI(rect);
        }

        public void UpdateViewData(IEnumerable<T> data)
        {
            _items = data;
            ReloadAndSort();
        }
        
        private void ReloadAndSort()
        {
            var currentSelected = state.selectedIDs;
            Reload();
            state.selectedIDs = currentSelected;
        }
        
        protected override void RowGUI(RowGUIArgs args)
        {
            var item = args.item as TableViewItem<T>;
            if(item == null) return;
            for (var visibleColumnIndex = 0; visibleColumnIndex < args.GetNumVisibleColumns(); visibleColumnIndex++)
            {
                var rect = args.GetCellRect(visibleColumnIndex);
                var columnIndex = args.GetColumn(visibleColumnIndex);

                var labelStyle = args.selected ? EditorStyles.whiteLabel : EditorStyles.label;
                labelStyle.alignment = TextAnchor.MiddleLeft;
                _columnDefs[columnIndex].onDraw?.Invoke(rect, item.Data);
            }
        }
    }
}