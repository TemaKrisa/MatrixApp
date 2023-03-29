using System;
using System.Collections.Generic;

namespace Matrix.Model;

public partial class EmailDatum
{
    public string EmailAdress { get; set; } = null!;

    public string AuthToken { get; set; } = null!;

    public string Host { get; set; } = null!;

    public int Port { get; set; }
}
