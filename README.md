# Performance Counter Minimal WebAPI (for Windows/Linux)

The purpose of this project is to create a minimal ASP.NET Core API providing information about performance counters (CPU, memory, disk I/O) on both Windows and Linux. 

The program collects performance counter data using the typeperf command on Windows and the vmstat command on Linux, with a one-second interval. The collected data is used to update the latest status and is provided to the client through HTTP GET for querying.

![Running on Windows](https://github.com/darkthread/perf-counter-min-webapi/blob/master/fig-windows.png?raw=true)

![Running on Linux](https://github.com/darkthread/perf-counter-min-webapi/blob/master/fig-linux.png?raw=true)
