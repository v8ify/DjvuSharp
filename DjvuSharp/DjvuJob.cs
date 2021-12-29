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

using Djvulibre.Internal;

namespace DjvuSharp
{
    /// <summary>
    /// Many essential ddjvuapi functions initiate asynchronous operations. 
    /// These "jobs" run in seperate threads and report their
    /// progress by posting messages into the ddjvu context event queue. 
    /// </summary>
    public class DjvuJob
    {
        private SWIGTYPE_p_ddjvu_job_s _ddjvu_job;

        internal DjvuJob(SWIGTYPE_p_ddjvu_job_s ddjvu_job)
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
                return (DDjvuStatus)djvulibre.ddjvu_job_status(_ddjvu_job);
            }
        }
    }
}