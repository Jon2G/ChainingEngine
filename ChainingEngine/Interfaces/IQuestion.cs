﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChainingEngine.Views;

namespace ChainingEngine.Interfaces
{
    public interface IQuestion
    {
        public bool Answer { get; set; }
        public string Question { get; set; }
        public void Ask(MainView mainView);
    }
}
