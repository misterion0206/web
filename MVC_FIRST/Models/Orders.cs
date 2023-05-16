using System;
using System.Collections.Generic;

namespace MVC_FIRST.Models;

public partial class Orders
{
    public int OrderID { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? CustomerID { get; set; }
}
