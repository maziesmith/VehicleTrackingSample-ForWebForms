using System.Collections.ObjectModel;

namespace ThinkGeo.MapSuite.VehicleTracking
{
    /// <summary>
    /// This class stands for a vehicle.
    /// </summary>
    public class Vehicle
    {
        private Collection<Location> historyLocations;

        private string motionStateIconVirtualPath;
        private bool isInFence;
        private double longitude;

        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }
        private double latitude;

        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        private Vehicle()
            : this(0)
        { }

        public Vehicle(int id)
        {
            this.Id = id;
            this.VehicleName = string.Empty;
            this.Location = new Location();
            this.historyLocations = new Collection<Location>();
        }

        public int Id
        {
            get;
            set;
        }

        public Location Location
        {
            get;
            set;
        }

        public Collection<Location> HistoryLocations
        {
            get { return historyLocations; }
        }

        public string VehicleName
        {
            get;
            set;
        }

        public string VehicleIconVirtualPath
        {
            get;
            set;
        }

        public bool IsInFence
        {
            get { return isInFence; }
            set { isInFence = value; }
        }

        public string MotionStateIconVirtualPath
        {
            get
            {
                if (MotionState == VehicleMotionState.Idle)
                {
                    motionStateIconVirtualPath = "Images/ball_gray.png";
                }
                else
                {
                    motionStateIconVirtualPath = "Images/ball_green.png";
                }
                return motionStateIconVirtualPath;
            }
        }

        /// <summary>
        /// If the Vehicle's speed is not 0 in the passed 4 minutes, we say it is in Motion. 
        /// </summary>
        /// <returns>State of current vehicle.</returns>
        public VehicleMotionState MotionState
        {
            get
            {
                VehicleMotionState vehicleState = VehicleMotionState.Idle;

                if (Location.Speed != 0)
                {
                    vehicleState = VehicleMotionState.Motion;
                }
                else
                {
                    int locationIndex = 0;
                    foreach (Location historyLocation in HistoryLocations)
                    {
                        if (locationIndex > 3)
                        {
                            break;
                        }
                        else if (historyLocation.Speed != 0)
                        {
                            vehicleState = VehicleMotionState.Motion;
                            break;
                        }
                        else
                        {
                            locationIndex++;
                        }
                    }
                }

                return vehicleState;
            }
        }

        public int SpeedDuration
        {
            get
            {
                int speedDuration = 0;
                double lastSpeed = Location.Speed;
                foreach (Location location in HistoryLocations)
                {
                    if (location.Speed == lastSpeed)
                    {
                        speedDuration++;
                    }
                    else
                    {
                        break;
                    }
                }
                return speedDuration;
            }
        }

        /// <summary>
        /// If the Vehicle's speed is not 0 in the passed 4 minutes, we say it is in Motion. 
        /// </summary>
        /// <returns>State of current vehicle.</returns>
        public VehicleMotionState GetCurrentState()
        {
            VehicleMotionState vehicleState = VehicleMotionState.Idle;

            if (Location.Speed != 0)
            {
                vehicleState = VehicleMotionState.Motion;
            }
            else
            {
                int locationIndex = 0;
                foreach (Location historyLocation in HistoryLocations)
                {
                    if (locationIndex > 3)
                    {
                        break;
                    }
                    else if (historyLocation.Speed != 0)
                    {
                        vehicleState = VehicleMotionState.Motion;
                        break;
                    }
                    else
                    {
                        locationIndex++;
                    }
                }
            }

            return vehicleState;
        }
    }
}
