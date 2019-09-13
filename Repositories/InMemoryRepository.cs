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
        private readonly IDictionary<Guid, TItem> _repo;
        public InMemoryRepository(IDictionary<Guid, TItem> repo){
            _repo = repo;
        }
        public async Task<IEnumerable<TItem>> All()
        {
            await Task.Delay(100).ConfigureAwait(false);
            return this;
        }
        public async Task<TItem> Find(Guid id)
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