using System;
using System.Linq;
using System.Collections.Generic;
using FCUUsbService.Models;

namespace FCUUsbService.Services;

internal class DhcpLeaseList : Tool
{
    public override string FileName { get; } = Constants.ToolConstants.DhcpLeaseList;
    public override string? Arguments => Constants.ToolConstants.DhcpLeaseListParsableQuery;

    public IEnumerable<string[]> Run()
    {
        Execute();

        if (StdOutput is null or {Length: <= 0}) return [];

        var split = StdOutput.Split("\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries));

        return split;
    }

    private void Execute() => base.Execute(Arguments);
}
