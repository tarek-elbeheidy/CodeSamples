using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;
using Microsoft.SharePoint;

namespace EJS.Cashing
{
    public class InMemoryCache : ICacheService
    {
        public T GetOrSet<T>(string cacheKey, Func<T> getItemCallback) where T : class
        {
            T item = null;
            if (MemoryCache.Default.Contains(cacheKey))
            {
                item = MemoryCache.Default.Get(cacheKey) as T;
            }

            if (item == null)
            {
                item = getItemCallback();
                MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddMinutes(100));

                if (!SPContext.Current.Site.RootWeb.AllProperties["CachedKeys"].ToString().Contains("," + cacheKey + ","))
                {
                    SPContext.Current.Site.RootWeb.AllProperties["CachedKeys"] += "," + cacheKey + ",";
                }
            }
            return item;
        }
    }


    //Example
    //var products=cacheService.GetOrSet("catalog.products", ()=>productRepository.GetAll())
}