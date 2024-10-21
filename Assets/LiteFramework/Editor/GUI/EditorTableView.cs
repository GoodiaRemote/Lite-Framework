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
        private MultiColumnHeader _multiColumnHeader;
        private readonly List<Column> _columnDefs = new();
        private MultiColumnHeaderState.Column[] _columns;
        private bool _isInitialized;
        private IEnumerable<T> _items;
        private List<TableViewItem<T>> _tableViewItems = new();
        private Func<T, string, bool> _searchFunc;

        public bool MultiSelect { get; set; }

        public float RowHeight
        {
            get => rowHeight;
            set => rowHeight = value;
        }

        public bool ShowAlternatingRowBackgrounds
        {
            get => showAlternatingRowBackgrounds;
            set => showAlternatingRowBackgrounds = value;
        }

        public bool ShowBorder
        {
            get => showBorder;
            set => showBorder = value;
        }
        
        public List<T> SelectedItems
        {
            get
            {
                if (state.selectedIDs.Count > 0)
                {
                    return _tableViewItems.Where(item => state.selectedIDs.Contains(item.id)).Select(item => item.Data).ToList();
                }

                return new List<T>();
            }
        }

        public delegate void DrawItem(Rect rect, T item);

        public class Column
        {
            internal MultiColumnHeaderState.Column column;
            internal DrawItem onDraw;
            internal Func<T, object> onSort;
            
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
            
            public Column SetSorting(Func<T, object> onSort)
            {
                this.onSort = onSort;
                column.canSort = true;
                return this;
            }

            public Column SetHeaderAlignment(TextAlignment textAlignment)
            {
                column.headerTextAlignment = textAlignment;
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
        }

        public void SetSearchFunc(Func<T, string, bool> searchFunc)
        {
            _searchFunc = searchFunc;
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
            multiColumnHeader.sortingChanged += HeaderSortingChanged;
            multiColumnHeader.ResizeToFit();
            multiColumnHeader.sortedColumnIndex = 0;
            Reload();
        }

        protected override bool CanMultiSelect(TreeViewItem item)
        {
            return MultiSelect;
        }

        protected override TreeViewItem BuildRoot()
        {
            var root = new TreeViewItem { depth = -1 };
            _tableViewItems.Clear();
            for (int i = 0; i < _items.Count(); i++)
            {
                var data = _items.ElementAt(i);
                _tableViewItems.Add(new TableViewItem<T>
                {
                    id = i,
                    Data = data,
                    depth = 0,
                });
                
            }
            root.children = _tableViewItems.Cast<TreeViewItem>().ToList();
            _isInitialized = true;
            return root;
        }

        public void DrawTableGUI(Rect rect, IEnumerable<T> data)
        {
            _items = data;
            ReloadAndSort();
            OnGUI(rect);
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
        
        private void ReloadAndSort()
        {
            var currentSelected = state.selectedIDs;
            HeaderSortingChanged(multiColumnHeader);
            Reload();
            state.selectedIDs = currentSelected;
        }
        

        protected override bool DoesItemMatchSearch(TreeViewItem item, string search)
        {
            return _searchFunc?.Invoke(((TableViewItem<T>)item).Data, search) ?? false;
        }

        protected override void RowGUI(RowGUIArgs args)
        {
            var item = args.item as TableViewItem<T>;
            if(item == null) return;
            for (var visibleColumnIndex = 0; visibleColumnIndex < args.GetNumVisibleColumns(); visibleColumnIndex++)
            {
                var rect = args.GetCellRect(visibleColumnIndex);
                CenterRectUsingSingleLineHeight(ref rect);
                var columnIndex = args.GetColumn(visibleColumnIndex);
                _columnDefs[columnIndex].onDraw?.Invoke(rect, item.Data);
            }
        }

        private void HeaderSortingChanged(MultiColumnHeader header)
        {
            if(header == null) return;
            var index = header.sortedColumnIndex;
            var ascending = header.IsSortedAscending(index);

            var sortCompare = _columnDefs[index]?.onSort;
            if (sortCompare != null)
            {
                var orderedItems = ascending ? _items.OrderBy(sortCompare) : _items.OrderByDescending(sortCompare);
                _items = orderedItems;
            }
        }
    }
}