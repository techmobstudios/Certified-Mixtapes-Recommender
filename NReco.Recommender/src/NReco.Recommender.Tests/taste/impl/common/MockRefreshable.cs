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


namespace NReco.CF.Taste.Impl.Common {

/// A mock {@link Refreshable} which counts the number of times it has been refreshed, for use in tests. 
sealed class MockRefreshable : IRefreshable {

  private int callCount;

  public void Refresh(IList<IRefreshable> alreadyRefreshed) {
    call();
  }

  public void call() {
    callCount++;
  }

  public int getCallCount() {
    return callCount;
  }

}

}