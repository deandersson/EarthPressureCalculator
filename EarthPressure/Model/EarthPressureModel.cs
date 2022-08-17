using System;
using System.Runtime.Serialization;

namespace EarthPressure.Model
{
    [DataContract]
    public class EarthPressureModel
    {
        // Properties

        [DataMember]
        public LOAD_TYPE SelectedLoadType;

        public enum LOAD_TYPE
        {
            ACTIVE_LOAD,
            LOAD_AT_REST
        }

        // Coefficients
        [DataMember]
        public double GammaM { get; set; }

        [DataMember]
        public double GammaRd { get; set; }

        // Materialdensities
        [DataMember]
        private double _gammaWater;

        [DataMember]
        private double _gammaPrime;
        public double GammaPrime
        {
            get { return _gammaPrime; }
            set
            {
                if (value >= 0)
                {
                    _gammaPrime = value;
                }
            }
        }

        [DataMember]
        private double _gammaPrimeU;
        public double GammaPrimeU
        {
            get { return _gammaPrimeU; }
            set
            {
                if (value >= 0)
                {
                    _gammaPrimeU = value;
                }
            }
        }

        // Waterlevel
        [DataMember]
        private double _uZ;
        public double UZ
        {
            get { return _uZ; }
            set
            {
                if (value >= 0)
                {
                    _uZ = value;
                }
            }
        }

        // Wallheight
        [DataMember]
        private double _h;
        public double H
        {
            get { return _h; }
            set
            {
                if (value >= 0)
                {
                    _h = value;
                }
            }
        }

        // Load on top of soil
        [DataMember]
        private double _q;
        public double Q
        {
            get { return _q; }
            set
            {
                if (value >= 0)
                {
                    _q = value;
                }
            }
        }

        // Soil properties
        [DataMember]
        private double _phi;
        [DataMember]
        private double _phiRad;
        public double Phi
        {
            get { return _phi; }
            set
            {
                if (value > 0)
                {
                    _phi = value;
                    _phiRad = ConvertDegreesToRadians(_phi);
                }
            }
        }

        private double PhiDRad
        {
            get
            {
                return Math.Atan(Math.Tan(_phiRad) / GammaM);
            }
        }
        public double PhiD
        {
            get
            {
                return ConvertRadiansToDegrees(PhiDRad);
            }
        }

        // Load factors
        public double K0
        {
            get
            {
                return 1 - Math.Sin(PhiDRad);
            }
        }
        public double Ka
        {
            get
            {
                return Math.Pow(Math.Tan(ConvertDegreesToRadians(45) - PhiDRad / 2), 2);
            }
        }
        

        // Constructor
        public EarthPressureModel()
        {
            // Setup a basic calculation
            GammaM = 1.3;
            GammaRd = 1.0;
            Phi = 30;
            _gammaWater = 10.0;
            _uZ = 2;
            _h = 4;
            _q = 0;
            _gammaPrime = 20;
            _gammaPrimeU = 14;
        }

        public double GetLoad(double z)
        {
            double load;

            double factor;
            switch (SelectedLoadType)
            {
                case LOAD_TYPE.ACTIVE_LOAD:
                    factor = Ka;
                    break;
                case LOAD_TYPE.LOAD_AT_REST:
                    factor = K0;
                    break;
                default:
                    factor = -1;
                    break;
            }

            if (z >= 0)
            {
                // Above water
                if (z <= _uZ)
                {
                    load = factor * _gammaPrime * z + factor * _q;
                }
                // Under water
                else
                {
                    double loadAboveWater = factor * _gammaPrime * _uZ;
                    double loadBelowWater = factor * _gammaPrimeU * (z - _uZ) + _gammaWater * (z - _uZ);
                    load = loadAboveWater + loadBelowWater + factor * _q;
                } 
            } 
            else
            {
                throw new ArgumentException("z cannot be negative");
            }

            return load;
        }

        // Helper functions
        private static double ConvertDegreesToRadians (double degrees)
        {
            double radians = (Math.PI / 180) * degrees;
            return radians;
        }

        private static double ConvertRadiansToDegrees(double radians)
        {
            double degrees = radians / (Math.PI / 180);
            return degrees;
        }
    }
}
