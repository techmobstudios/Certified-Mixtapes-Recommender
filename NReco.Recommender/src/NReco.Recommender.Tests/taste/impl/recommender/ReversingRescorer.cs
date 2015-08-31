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
using NReco.CF.Taste.Recommender;

namespace NReco.CF.Taste.Impl.Recommender {

/// <p>Simple {@link Rescorer} which negates the given score, thus reversing order of rankings.</p> 
public sealed class ReversingRescorer<T> : IRescorer<T>, IDRescorer {

  public double Rescore(T thing, double originalScore) {
    return -originalScore;
  }

  public bool IsFiltered(T thing) {
    return false;
  }

  public double rescore(long ID, double originalScore) {
    return -originalScore;
  }

  public bool isFiltered(long ID) {
    return false;
  }

}

}