﻿using System;
using System.Runtime.InteropServices;

public partial class NNanomsg
{
    public const int NN_DONTWAIT = 1;

    public const int AF_SP = 1;
    public const int AF_SP_RAW = 2;

    public const int NN_LINGER = 1;
    public const int NN_SNDBUF = 2;
    public const int NN_RCVBUF = 3;
    public const int NN_SNDTIMEO = 4;
    public const int NN_RCVTIMEO = 5;
    public const int NN_RECONNECT_IVL = 6;
    public const int NN_RECONNECT_IVL_MAX = 7;
    public const int NN_SNDPRIO = 8;
    public const int NN_SNDFD = 10;
    public const int NN_RCVFD = 11;
    public const int NN_DOMAIN = 12;
    public const int NN_PROTOCOL = 13;
    public const int NN_IPV4ONLY = 14;

    public const int NN_PROTO_REQREP = 3;
    public const int NN_REQ = NN_PROTO_REQREP*16 + 0;
    public const int NN_REP = NN_PROTO_REQREP*16 + 1;


    public static int Socket(int domain, int protocol)
    {
        return UsingWindows
                   ? Interop_Windows.nn_socket(domain, protocol)
                   : Interop_Linux.nn_socket(domain, protocol);
    }

    public static int Connect(int s, string addr)
    {
        return UsingWindows ? Interop_Windows.nn_connect(s, addr + '\0') : Interop_Linux.nn_connect(s, addr + '\0');
    }

    public static int Bind(int s, string addr)
    {
        return UsingWindows ? Interop_Windows.nn_bind(s, addr + '\0') : Interop_Linux.nn_bind(s, addr + '\0');
    }

    public static int SetSocketOpt(int s, int level, int option, string val)
    {
        // todo: unsure if \0 termination is necessary. remove if not.
        return UsingWindows
                   ? Interop_Windows.nn_setsockopt_string(s, level, option, val + '\0', val.Length)
                   : Interop_Linux.nn_setsockopt_string(s, level, option, val + '\0', val.Length);
    }

    public static int SetSocketOpt(int s, int level, int option, int val)
    {
        return UsingWindows
                   ? Interop_Windows.nn_setsockopt_int(s, level, option, ref val, sizeof(int))
                   : Interop_Linux.nn_setsockopt_int(s, level, option, ref val, sizeof(int));        
    }

    public static int GetSocketOpt(int s, int level, int option, ref int val)
    {
        
    }

    public static int GetSocketOpt(int s, int level, int option, ref string val)
    {
        
    }

    public static int Recv(int s, byte[] buf, int flags)
    {
        return UsingWindows
                   ? Interop_Windows.nn_recv(s, buf, buf.Length, flags)
                   : Interop_Linux.nn_recv(s, buf, buf.Length, flags);
    }

    public static int Send(int s, byte[] buf, int flags)
    {
        return UsingWindows
                   ? Interop_Windows.nn_send(s, buf, buf.Length, flags)
                   : Interop_Linux.nn_send(s, buf, buf.Length, flags);
    }

    public static int Errno()
    {
        return UsingWindows
                   ? Interop_Windows.nn_errno()
                   : Interop_Linux.nn_errno();
    }

    public static int Close(int s)
    {
        return UsingWindows
                   ? Interop_Windows.nn_close(s)
                   : Interop_Linux.nn_close(s);
    }

    public static int Shutdown(int s, int how)
    {
        return UsingWindows
                   ? Interop_Windows.nn_shutdown(s, how)
                   : Interop_Linux.nn_shutdown(s, how);
    }

    public static int Device(int s1, int s2)
    {
        return UsingWindows
                   ? Interop_Windows.nn_device(s1, s2)
                   : Interop_Linux.nn_device(s1, s2);
    }

    public static void Term()
    {
        if (UsingWindows)
        {
            Interop_Windows.nn_term();
        }
        else
        {
            Interop_Linux.nn_term();
        }
    }

    public static string StrError(int errnum)
    {
        return UsingWindows
                   ? Marshal.PtrToStringAnsi(Interop_Windows.nn_strerror(errnum))
                   : Marshal.PtrToStringAnsi(Interop_Linux.nn_strerror(errnum));
    }

    private static bool UsingWindows
    {
        get
        {
            if (_usingWindows == null)
            {
                string os_s = Environment.OSVersion.ToString().ToLower();
                _usingWindows = os_s.Contains("windows");
            }

            return _usingWindows.Value;
        }
    }
    private static bool? _usingWindows;
}
