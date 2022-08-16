using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;
using EarthPressure.Model;

namespace EarthPressure.ViewModel
{
    internal class EarthPressureViewModel : ObservableObject
    {

        private EarthPressureModel _model;

        public EarthPressureModel Model { get { return _model; }}

        public EarthPressureViewModel()
        {
            _model = new EarthPressureModel();
            H = 4;
            UZ = 3;
            OnPropertyChanged(null);
        }

        public double GammaRd
        {
            get { return _model.GammaRd; }
            set { _model.GammaRd = value; OnPropertyChanged(null); }
        }

        public double GammaM
        {
            get { return _model.GammaM; }
            set { _model.GammaM = value; OnPropertyChanged(null); }
        }

        private double _h;
        public double H
        {
            get
            {
                return _h;
            }
            set
            {
                _h = value; OnPropertyChanged(null);
            }
        }

        private double _uz;
        public double UZ
        {
            get
            {
                return _uz;
            }
            set
            {
                _uz = value; OnPropertyChanged(null);
            }
        }
        public double Hmm
        {
            get
            {
                return _h*1000;
            }
        }

        public double GammaPrime
        {
            get
            {
                return _model.GammaPrime;
            }
            set
            {
                _model.GammaPrime = value;
                OnPropertyChanged(null);
            }
        }

        public double GammaPrimeU
        {
            get { return (_model.GammaPrimeU); }
            set { _model.GammaPrimeU = value; OnPropertyChanged(null); }
        }

        public double Phi
        {
            get { return _model.Phi; }
            set
            {
                _model.Phi = value;
                OnPropertyChanged(null);
            }
        }

        public double PhiD
        {
            get { return _model.PhiD; }
        }

        public bool IsWaterAboveBottom
        {
            get
            {
                return UZ < H;
            }
        }

        public string PhiD_Formula
        {
            get { return $"\\varphi_d = atan \\left( \\frac{{tan(\\varphi)}}{{\\gamma_M}} \\right) = {_model.PhiD:F3} deg"; }
        }

        public string K0_Formula
        {
            get { return $"K_0 = 1 - sin(\\varphi_d) = {_model.K0:F3}"; }
        }

        public string Ka_Formula
        {
            get { return $"K_a = tan \\left( 45 deg - \\frac{{\\varphi_d}}{{2}} \\right) = {_model.Ka:F3}"; }
        }

        public string LoadAtTop_Formula
        {
            get { return $"Q_0 = {_model.getActiveLoad(0):F3} kN/m^2"; }
        }

        public string LoadAtWater_Formula
        {
            get { return $"Q_U_z = {_model.getActiveLoad(UZ):F3} kN/m^2"; }
        }

        public string LoadAtBottom_Formula
        {
            get { return $"Q_H = {_model.getActiveLoad(H):F3} kN/m^2"; }
        }


        private double scale = 1;

        // Drawing properties
        public PathGeometry WallGeometry
        {
            get
            {
                PathGeometry _geometry = new PathGeometry();

                // Wall figure
                List<LineSegment> segments = new List<LineSegment>();
                segments.Add(new LineSegment(new Point(200,0), true));
                segments.Add(new LineSegment(new Point(200, H*1000), true));
                segments.Add(new LineSegment(new Point(0, H*1000), true));
                _geometry.Figures.Add(new PathFigure(new Point(0, 0), segments, true));

                return _geometry;
            }
        }

        public PathGeometry LoadGeometryNoWater
        {
            get
            {
                PathGeometry _geometry = new PathGeometry();

                // Wall figure
                List<LineSegment> segments = new List<LineSegment>();
                segments.Add(new LineSegment(new Point(_model.getActiveLoad(H)*scale, 500), true));
                segments.Add(new LineSegment(new Point(0, 500), true));
                _geometry.Figures.Add(new PathFigure(new Point(0, 0), segments, true));

                return _geometry;
            }
        }

        public PathGeometry LoadGeometryWithWater
        {
            get
            {
                PathGeometry _geometry = new PathGeometry();

                // Wall figure
                List<PathSegment> segments = new List<PathSegment>();
                segments.Add(new LineSegment(new Point(_model.getActiveLoad(UZ) * scale, WaterLevelPosition), true));
                segments.Add(new LineSegment(new Point(0, WaterLevelPosition), true));
                segments.Add(new LineSegment(new Point(_model.getActiveLoad(UZ) * scale, WaterLevelPosition), false));
                segments.Add(new LineSegment(new Point(_model.getActiveLoad(H) * scale, 500), true));
                segments.Add(new LineSegment(new Point(0, 500), true));
                _geometry.Figures.Add(new PathFigure(new Point(0, 0), segments, true));

                return _geometry;
            }
        }

        public double WaterLevelPosition
        {
            get
            {
                return 500 * UZ / H;
            }
        }

        public double CanvasHeight
        {
            get
            {
                return H*1000;
            }
        }

        public double CanvasWidth
        {
            get
            {
                return 2000;
            }
        }

        public double CanvasFontSize
        {
            get
            {
                return H/2 * 50;
            }
        }

        public double WallHeight
        {
            get
            {
                return 500;
            }
        }

        public double WallWidth
        {
            get
            {
                return 500 / (H*5);
            }
        }
    }
}
