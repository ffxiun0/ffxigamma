using System;

namespace ffxigamma {
    [Serializable]
    class VolumeControlException : Exception {
        public VolumeControlException() { }
        public VolumeControlException(string message)
            : base(message) { }
        public VolumeControlException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    [Serializable]
    class VolumeControlNotFoundException : VolumeControlException {
        public VolumeControlNotFoundException() { }
        public VolumeControlNotFoundException(string message)
            : base(message) { }
        public VolumeControlNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    [Serializable]
    class VolumeControlNotSupportedException : VolumeControlException {
        public VolumeControlNotSupportedException() { }
        public VolumeControlNotSupportedException(string message)
            : base(message) { }
        public VolumeControlNotSupportedException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
