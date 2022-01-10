/*
*   DjvuSharp - .NET bindings for DjvuLibre
*   Copyright (C) 2021 Prajwal Jadhav
*   
*   This program is free software; you can redistribute it and/or
*   modify it under the terms of the GNU General Public License
*   as published by the Free Software Foundation; either version 2
*   of the License, or (at your option) any later version.
*   
*   This program is distributed in the hope that it will be useful,
*   but WITHOUT ANY WARRANTY; without even the implied warranty of
*   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*   GNU General Public License for more details.
*   
*   You should have received a copy of the GNU General Public License
*   along with this program; if not, write to the Free Software
*   Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/

using System; // IDisposable

namespace DjvuSharp
{
    /// <summary>
    /// Many essential ddjvuapi functions initiate asynchronous operations. 
    /// These "jobs" run in seperate threads and report their
    /// progress by posting messages into the ddjvu context event queue. 
    /// </summary>
    public class DjvuJob: IDisposable
    {
        private IntPtr _ddjvu_job;
        private bool disposedValue;

        internal DjvuJob(IntPtr ddjvu_job)
        {
            _ddjvu_job = ddjvu_job;
        }

        /// <summary>
        /// Returns the status of the specified this job.
        /// </summary>
        public DDjvuStatus Status
        {
            get
            {
                return (DDjvuStatus)Native.ddjvu_job_status(_ddjvu_job);
            }
        }

        /// <summary>
        /// Indicates if this job has finished.
        /// </summary>
        /// <returns>true if the job has finished; false otherwise</returns>
        public bool IsDone()
        {
            return this.Status >= DDjvuStatus.DDJVU_JOB_OK;
        }

        /// <summary>
        /// Indicates if any errors occured while running this job.
        /// </summary>
        /// <returns>true if error occured; false otherwise.</returns>
        public bool HadErrors()
        {
            return this.Status >= DDjvuStatus.DDJVU_JOB_FAILED;
        }

        /// <summary>
        /// Attempts to cancel the specified job.
        /// <para>
        /// This is a best effort function. There no guarantee that the job will 
        /// actually stop.
        /// </para>
        /// </summary>
        public void Stop()
        {
            Native.ddjvu_job_stop(_ddjvu_job);
        }

        // Implementation of Dispose pattern

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                Native.ddjvu_job_release(_ddjvu_job);
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        ~DjvuJob()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
