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

namespace DjvuSharp.Enums
{
    public enum JobStatus
    {
        /// <summary>
        /// Operation was not even started
        /// </summary>
        JOB_NOTSTARTED,

        /// <summary>
        /// Operation is in progress
        /// </summary>
        JOB_STARTED,

        /// <summary>
        /// Operation terminated successfully
        /// </summary>
        JOB_OK,

        /// <summary>
        /// Operation failed because of an error
        /// </summary>
        JOB_FAILED,

        /// <summary>
        /// Operation was interrupted by user
        /// </summary>
        JOB_STOPPED
    }
}
