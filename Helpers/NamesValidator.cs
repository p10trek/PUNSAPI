﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PunsApi.Helpers
{
    public static class NamesValidator
    {
        public static bool IsNameValid(string roomName)
        {
            return roomName.Length > 2 ? true : false;
        }
    }
}
