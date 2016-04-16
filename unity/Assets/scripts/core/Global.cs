using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using Rf.Models;

namespace Rf.Core {
    public class Global : MonoSingleton<Global> {
        public LineModel LineModel;

        public void Awake() {
            LineModel = GetComponentInChildren<LineModel>();
        }
    }

}
