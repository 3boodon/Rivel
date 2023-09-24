﻿using System;

namespace Rivel.Models {
    public abstract class BaseModel {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
