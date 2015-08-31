/*
 *  Copyright 2013-2014 Vitalii Fedorchenko (nrecosite.com)
 *
 *  Licensed under Source Code Licence (see LICENSE file).
 *  
 *  Parts of this code are based on Apache Mahout ("Taste") that was licensed under the
 *  Apache 2.0 License (see http://www.apache.org/licenses/LICENSE-2.0).
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS 
 *  OF ANY KIND, either express or implied.
 */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NReco.CF.Taste.Common;
using NReco.CF.Taste.Impl;
using NReco.CF;
using NUnit.Framework;

namespace NReco.CF.Taste.Impl.Common {


public sealed class CacheTest : TasteTestCase {

  [Test]
  public void testLotsOfGets() {
    IRetriever<Object,Object> retriever = new IdentityRetriever();
    Cache<Object,Object> cache = new Cache<Object,Object>(retriever, 1000);
    for (int i = 0; i < 1000000; i++) {
      Assert.AreEqual(i, cache.Get(i));
    }
  }

  [Test]
  public void testMixedUsage() {
    var random = RandomUtils.getRandom();
    IRetriever<Object,Object> retriever = new IdentityRetriever();
    Cache<Object,Object> cache = new Cache<Object,Object>(retriever, 1000);
    for (int i = 0; i < 1000000; i++) {
      double r = random.nextDouble();
      if (r < 0.01) {
        cache.Clear();
      } else if (r < 0.1) {
        cache.Remove(r - 100);
      } else {
        Assert.AreEqual(i, cache.Get(i));
      }
    }
  }
  
  private class IdentityRetriever : IRetriever<Object,Object> {
    public Object Get(Object key) {
      return key;
    }
  }
}
}