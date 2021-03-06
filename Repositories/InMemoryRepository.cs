using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using productsWebapi.Products;

namespace productsWebapi.Repositories
{
    public sealed class InMemoryRepository<TItem> : IMutableRepository<TItem>
        where TItem: IIdentifiable
    {
        private readonly IDictionary<String, TItem> _repo;
        public InMemoryRepository(IDictionary<String, TItem> repo){
            _repo = repo; // Technically we'd need to copy the dict here.
        }
        public async Task<IEnumerable<TItem>> All()
        {
            await Task.Delay(100).ConfigureAwait(false);
            return this;
        }
        public async Task<TItem> Find(String id)
        {
            await Task.Delay(60).ConfigureAwait(false);
            _repo.TryGetValue(id, out TItem item);
            return item;
        }
        public async Task<TItem> Add(TItem item)
        {
            await Task.Delay(120).ConfigureAwait(false);
            _repo.Add(item.Id, item);
            return item;
        }
        public IEnumerator<TItem> GetEnumerator(){
            foreach (TItem item in _repo.Values)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}