using System.Runtime.InteropServices;
using System.Text;

namespace Fantasy;

public class MemoryAccessor : IDisposable
{
private readonly int _processAllAccess = 0x1F0FFF;

private readonly IntPtr _handle;

public MemoryAccessor(int pid)
{

    _handle = Open(pid);
    if (_handle == IntPtr.Zero)
    {
        throw new ApplicationException("unable to get handle");
    }

}

[DllImport("Kernel32.dll")]
private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

[DllImport("Kernel32.dll")]
private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, uint nSize, out int lpNumberOfBytesRead);

[DllImport("Kernel32.dll")]
private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, uint nSize, out int lpNumberOfBytesWritten);


[DllImport("Kernel32.dll")]
private static extern bool CloseHandle(IntPtr hObject);


public IntPtr Open(int pid) => OpenProcess(_processAllAccess, false, pid);

public bool Read()
{
    return true;
}

public bool Write(IntPtr address, [Out] byte[] buffer, out int bytesWritten)
{
    return WriteProcessMemory(_handle, address, buffer, (uint) buffer.Length, out bytesWritten);
}


public void Dispose()
{
    CloseHandle(_handle);
}
}