using System;
using System.Collections.Generic;
using UnityEngine;

namespace RiskSrc.View.Component {
    public interface IRiskViewPanel {

        bool isActive { get; }
        bool isUpToDate { get; }
        void Show();
        void Hide();
    }
}