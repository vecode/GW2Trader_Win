// This class is based on an implementation from http://paginatedcollection.codeplex.com/. 
// Paginatedcollection is licensed under the Microsoft Public License (MS-PL) which requires a full copy of the license when
// distributing source code.

// Microsoft Public License (MS-PL)

// This license governs use of the accompanying software. If you use the software, you
// accept this license. If you do not accept the license, do not use the software.

// 1. Definitions
// The terms "reproduce," "reproduction," "derivative works," and "distribution" have the
// same meaning here as under U.S. copyright law.
// A "contribution" is the original software, or any additions or changes to the software.
// A "contributor" is any person that distributes its contribution under this license.
// "Licensed patents" are a contributor's patent claims that read directly on its contribution.

// 2. Grant of Rights
// (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
// (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.

// 3. Conditions and Limitations
// (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
// (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.
// (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.
// (D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
// (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2Trader.MVVM
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Collections.Specialized;
    using System.Collections.Generic;

    /// <summary>
    /// This class represents a single Page collection, but have the entire items available inside
    /// </summary>

    public class PaginatedObservableCollection<T> : ObservableCollection<T>
    {
        private List<T> _originalCollection;

        private int _pageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                if (value >= 0)
                {
                    _pageSize = value;
                    RecalculateThePageItems();
                    OnPropertyChanged(new PropertyChangedEventArgs("PageSize"));
                }
            }
        }

        private int _currentPage;
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                if (value >= 0 && value <= PageCount)
                {
                    _currentPage = value;
                    RecalculateThePageItems();
                    OnPropertyChanged(new PropertyChangedEventArgs("CurrentPage"));
                }
            }
        }

        private int _pageCount;
        public int PageCount
        {
            get
            {
                return _pageCount;
            }
            private set
            {
                _pageCount = value;
                OnPropertyChanged(new PropertyChangedEventArgs("PageCount"));
            }
        }

        private Predicate<T> _filter;
        public Predicate<T> Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                _filter = value;
                CurrentPage = 0;
            }
        }

        public PaginatedObservableCollection(IEnumerable<T> collection, int pageSize = 50)
        {
            _currentPage = 0;
            _pageSize = pageSize;
            _originalCollection = new List<T>(collection);
            RecalculateThePageItems();
            RecalculatePageCount();
        }

        public PaginatedObservableCollection(int pageSize)
        {
            _currentPage = 0;
            _pageSize = pageSize;
            _originalCollection = new List<T>();
            RecalculatePageCount();
        }

        public PaginatedObservableCollection()
        {
            _currentPage = 0;
            _pageSize = 1;
            _originalCollection = new List<T>(); 
            RecalculatePageCount();
        }

        public void MoveToNextPage()
        {
            CurrentPage += 1;
        }

        public void MoveToPreviousPage()
        {
            CurrentPage -= 1;
        }

        public bool CanMoveToNextPage()
        {
            return _currentPage < _pageCount - 1;
        }

        public bool CanMoveToPreviousPage()
        {
            return _currentPage > 0;
        }

        protected override void InsertItem(int index, T item)
        {
            throw new NotSupportedException("InsertItem is not supported.");
        }

        protected override void RemoveItem(int index)
        {
            throw new NotSupportedException("RemoveItem is not supported.");
        }       

        private void RecalculateThePageItems()
        {
            Clear();

            int startIndex = _currentPage * _pageSize;

            List<T> filteredItems = _filter == null ? _originalCollection : _originalCollection.FindAll(_filter);

            for (int i = startIndex; i < startIndex + _pageSize; i++)
            {
                if (filteredItems.Count > i)
                {
                    base.InsertItem(i - startIndex, filteredItems[i]);
                }
            }
        }

        private void RecalculatePageCount()
        {
            if (_filter == null)
            {
                PageCount = (int)Math.Ceiling(_originalCollection.Count / _pageSize * 1.0f);
            }
            else
            {
                PageCount = (int)Math.Ceiling(_originalCollection.FindAll(_filter).Count / _pageSize * 1.0f);
            }
        }
    }
}

