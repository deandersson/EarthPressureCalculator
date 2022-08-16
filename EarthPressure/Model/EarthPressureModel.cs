using System;

namespace EarthPressure.Model
{
    public class EarthPressureModel
    {
        // Properties
        private double _gammaWater = 10.0;
        private double _gammaM = 1.3;
        public double GammaM
        {
            get
            {
                return _gammaM;
            }
            set
            {
                _gammaM = value;
                updateProperties();
            }
        }
        private double _gammaRd = 1.0;
        public double GammaRd
        {
            get
            {
                return _gammaRd;
            }
            set
            {
                _gammaRd = value;
                updateProperties();
            }
        }
        private double _uZ = 2;
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
        private double _q = 0;
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
        private double _gammaPrime = 20;
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
        private double _gammaPrimeU = 14;
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
        private double _phi;
        private double _phiRad;
        private double _phiDRad;
        public double Phi
        {
            get { return _phi; }
            set
            {
                if (value > 0)
                {
                    _phi = value;
                    updateProperties();
                }
            }
        }
        private double _phiD;
        public double PhiD
        {
            get
            {
                return _phiD;
                ;
            }
            private set
            {
                _phiD = value;
            }
        }

        public double K0 { get; private set; }
        public double Ka { get; private set; }

        private void updateProperties()
        {
            _phiRad = ConvertDegreesToRadians(_phi);
            _phiDRad = Math.Atan(Math.Tan(_phiRad) / GammaM);
            PhiD = ConvertRadiansToDegrees(_phiDRad);
            K0 = 1 - Math.Sin(_phiDRad);
            Ka = Math.Pow(Math.Tan(ConvertDegreesToRadians(45) - _phiDRad / 2), 2);
        }
        

        // Constructor
        public EarthPressureModel()
        {
            Phi = 30;
        }

        public double getLoadAtRest(double z)
        {
            return getLoad(z, K0);
        }

        public double getActiveLoad(double z)
        {
            return getLoad(z, Ka);
        }

        private double getLoad(double z, double factor)
        {
            double load = -1;
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
