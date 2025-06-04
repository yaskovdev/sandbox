namespace LoadBalancer;

public enum WorkerStatus
{
    Idle,
    Busy,

    /// <summary>
    /// Indicates that a worker is temporarily reserved. In this state, the worker is neither available 
    /// for new tasks nor can have its status updated by the poller.
    /// </summary>
    Reserved
}
