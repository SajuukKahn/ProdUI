using System.Collections.ObjectModel;
using ProdData.Interfaces;

namespace ProdData.Extensions
{
    public class IndexedObservableCollection<T> : ObservableCollection<T> where T : IIndexAware
    {
        public IndexedObservableCollection()
        {
            CollectionChanged += (s, e) => TestClass_CollectionChanged();
        }

        private void TestClass_CollectionChanged()
        {
            foreach (var item in this)
            {
                item.Ordinal = IndexOf(item);
            }
        }
    }
}