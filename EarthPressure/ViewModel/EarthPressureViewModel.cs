using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;
using EarthPressure.Model;
using EarthPressureCalculator.Commands;
using System.Windows.Input;

namespace EarthPressure.ViewModel
{
    public class EarthPressureViewModel : ViewModelBase
    {


        private EarthPressureModel _model;

        public ICommand SelectActiveLoadCommand { get; }
        public ICommand SelectLoadAtRestCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand LoadCommand { get; }
        public ICommand SaveAsCommand { get; }
        public ICommand ExitCommand { get; }
        public ICommand PrintCommand { get; }

        public string GetCurrentUser
        {
            get
            {
                string displayName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                return displayName;
            }
        }

        public string WindowTitle
        {
            get
            {
                if (FileName == null)
                {
                    return "Earthpressure: New file..";
                }

                return "EarthPressure: " + FileName + (IsSaved ? "" : "*"); 
            }
        }

        private bool _isSaved;
        public bool IsSaved
        {
            get
            {
                return _isSaved;
            }
            set
            {
                _isSaved = value;
                OnPropertyChanged(nameof(WindowTitle));
            }
        }

        private string? _fileName;
        public string? FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
                OnPropertyChanged(nameof(WindowTitle));
            }
        }

        public EarthPressureViewModel(EarthPressureModel model)
        {
            _isSaved = false;
            _model = model;
            SelectActiveLoadCommand = new RelayCommand(SetActiveLoad);
            SelectLoadAtRestCommand = new RelayCommand(SetLoadAtRest);
            SaveCommand = new SaveCommand(this, _model, false);
            LoadCommand = new LoadCommand(this);
            SaveAsCommand = new SaveCommand(this, _model, true);
            ExitCommand = new RelayCommand(Exit);
            PrintCommand = new PrintCommand(this);
        }

        private void SetActiveLoad(object parameter) => SetLoadType(EarthPressureModel.LOAD_TYPE.ACTIVE_LOAD);
        private void SetLoadAtRest(object parameter) => SetLoadType(EarthPressureModel.LOAD_TYPE.LOAD_AT_REST);
        private void SetLoadType(EarthPressureModel.LOAD_TYPE loadType)
        {
            _model.SelectedLoadType = loadType;
            OnPropertyChanged(null);
        }

        private void Exit(object parameter)
        {

        }

        public EarthPressureModel Model
        {
            get
            {
                return _model;
            }
            set
            {
                _model = value;
                OnPropertyChanged(null);
            }
        }

        // Input data properties
        public string Name
        {
            get
            {
                return _model.Name;
            }
            set
            {
                _model.Name = value;
            }
        }
        public string ProjectName
        {
            get
            {
                return _model.ProjectName;
            }
            set
            {
                _model.ProjectName = value;
            }
        }
        public double GammaW
        {
            get
            {
                return _model.GammaW;
            }
            set
            {
                _model.GammaW = value;
                IsSaved = false;
                OnPropertyChanged(null);
            }
        }
        public double GammaRd
        {
            get
            {
                return _model.GammaRd;
            }
            set
            {
                _model.GammaRd = value;
                IsSaved = false;
                OnPropertyChanged(null);
            }
        }
        public double GammaM
        {
            get
            {
                return _model.GammaM;
            }
            set
            {
                _model.GammaM = value;
                IsSaved = false;
                OnPropertyChanged(null);
            }
        }
        public double H
        {
            get
            {
                return _model.H;
            }
            set
            {
                _model.H = value;
                IsSaved = false;
                OnPropertyChanged(null);
            }
        }
        public double Q
        {
            get
            {
                return _model.Q;
            }
            set
            {
                _model.Q = value;
                IsSaved = false;
                OnPropertyChanged(null);
            }
        }
        public double UZ
        {
            get
            {
                return _model.UZ;
            }
            set
            {
                _model.UZ = value;
                IsSaved = false;
                OnPropertyChanged(null);
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
                IsSaved = false;
                OnPropertyChanged(null);
            }
        }
        public double GammaPrimeU
        {
            get
            {
                return (_model.GammaPrimeU);
            }
            set
            {
                _model.GammaPrimeU = value;
                IsSaved = false;
                OnPropertyChanged(null);
            }
        }
        public double Phi
        {
            get
            {
                return _model.Phi;
            }
            set
            {
                _model.Phi = value;
                IsSaved = false;
                OnPropertyChanged(null);
            }
        }

        // Formulas
        #region Formulas
        public string PhiD_Formula
        {
            get { return $"\\varphi_d = atan \\left( \\frac{{tan(\\varphi)}}{{\\gamma_M}} \\right) = {_model.PhiD:F3} deg"; }
        }
        public string K_Formula
        {
            get
            {
                switch (_model.SelectedLoadType)
                {
                    case EarthPressureModel.LOAD_TYPE.ACTIVE_LOAD:
                        return Ka_Formula;
                    case EarthPressureModel.LOAD_TYPE.LOAD_AT_REST:
                        return K0_Formula;
                    default:
                        return "ERROR";
                }
            }
        }
        public string K0_Formula
        {
            get { return $"K_0 = 1 - sin(\\varphi_d) = {_model.K0:F3}"; }
        }
        public string Ka_Formula
        {
            get { return $"K_a = tan \\left( 45 deg - \\frac{{\\varphi_d}}{{2}} \\right) = {_model.Ka:F3}"; }
        }
        public string z_Formula
        {
            get
            {
                return $"z = {_model.H:F3}m";
            }
        }
        public string Uz_Formula
        {
            get
            {
                return $"U_z = {_model.UZ:F3}m";
            }
        }
        public string Q_Formula
        {
            get
            {
                return $"q = {_model.Q:F3}kN/m^2";
            }
        }
        public string LoadAtTop_Formula
        {
            get { return $"Q_0 = {_model.GetLoad(0):F3} kN/m^2"; }
        }
        public string LoadAtWater_Formula
        {
            get { return $"Q_U_z = {_model.GetLoad(_model.UZ):F3} kN/m^2"; }
        }
        public string LoadAtBottom_Formula
        {
            get { return $"Q_H = {_model.GetLoad(_model.H):F3} kN/m^2"; }
        }

        public string SelectedKSymbol_Formula
        {
            get
            
            {
                switch (_model.SelectedLoadType)
                {
                    case EarthPressureModel.LOAD_TYPE.ACTIVE_LOAD:
                        return "K_a";
                    case EarthPressureModel.LOAD_TYPE.LOAD_AT_REST:
                        return "K_0";
                    default:
                        return "ERROR";
                }
            }
            
        }

        public string InputData_Print_Formula
        {
            get
            {
                return $"\\gamma_R_d={GammaRd:F2}\\text{{    }}\\gamma_M={GammaM:F2}\\text{{    }}\\gamma'={GammaPrime}\\text{{    }}\\gamma'_u={GammaPrimeU}\\text{{    }}\\varphi={Phi:F3}\\text{{   }}q = {Q:F3}";
            }
        }
        public string K_Print_Formula
        {
            get
            {
                switch (_model.SelectedLoadType)
                {
                    case EarthPressureModel.LOAD_TYPE.ACTIVE_LOAD:
                        return $"K_a = tan \\left( 45 deg - \\frac{{\\varphi_d}}{{2}} \\right) = tan \\left( 45 deg - \\frac{{{_model.PhiD:F3}}}{{2}} \\right) = {_model.Ka:F3} \\text{{     (Active load)}}";
                    case EarthPressureModel.LOAD_TYPE.LOAD_AT_REST:
                        return $"K_0 = 1 - sin(\\varphi_d) = 1 - sin({_model.PhiD:F3}) = {_model.K0:F3} \\\\text{{     (Load at rest)}}\"";
                    default:
                        return "ERROR";
                }
            }
        }
        public string PhiD_Print_Formula
        {
            get { return $"\\varphi_d = atan \\left( \\frac{{tan(\\varphi)}}{{\\gamma_M}} \\right) = atan \\left( \\frac{{tan({_model.Phi:F3})}}{{{_model.GammaM:F3}}} \\right) = {_model.PhiD:F3} deg"; }
        }
        public string LoadAtTop_Print_Formula
        {
            get { return $"Q_0 = {SelectedKSymbol_Formula} * \\gamma' * z + {SelectedKSymbol_Formula} * q \\\\ \\text{{     }} = {_model.GetFactor():F3} * {GammaPrime:F2} * {H:F3} + {_model.GetFactor():F3} * {Q:F3} \\\\ \\text{{     }} = {_model.GetLoad(0):F3} kN/m^2"; }
        }
        public string LoadAtWater_Print_Formula
        {
            get { return $"Q_U_z = {SelectedKSymbol_Formula} * \\gamma' * z + {SelectedKSymbol_Formula} * q \\\\ \\text{{      }} = {_model.GetFactor():F3} * {GammaPrime:F2} * {H:F3} + {_model.GetFactor():F3} * {Q:F3} \\\\ \\text{{      }} = {_model.GetLoad(_model.UZ):F3} kN/m^2"; }

        }
        public string LoadAtBottomWithoutWater_Print_Formula
        {
            get { return $"Q_H = {SelectedKSymbol_Formula} * \\gamma' * z + {SelectedKSymbol_Formula} * q \\\\ \\text{{     }} = {_model.GetFactor():F3} * {GammaPrime:F2} * {H:F3} + {_model.GetFactor():F3} * {Q:F3} \\\\ \\text{{     }} = {_model.GetLoad(_model.H):F3} kN/m^2"; }
        }
        public string LoadAtBottomWithWater_Print_Formula
        {
            get { return $"Q_H = {SelectedKSymbol_Formula} * \\left( \\gamma' * U_z + \\gamma'_u * (z-U_z) + q \\right) + \\gamma_w * (z-U_z) \\\\ \\text{{     }} = {_model.GetFactor():F3} * \\left( {GammaPrime:F2} * {UZ:F3} + {GammaPrimeU:F2} * ({H:F3} - {UZ:F3} + {Q:F3}) \\right) + {GammaW:F1} * ({H:F3}-{UZ:F3}) \\\\ \\text{{     }} = {_model.GetLoad(_model.H):F3} kN/m^2"; }
        }
        #endregion

        // Graphics properties
        private double scale = 1;
        public PathGeometry LoadGeometryNoWater
        {
            get
            {
                PathGeometry _geometry = new PathGeometry();

                List<LineSegment> segments = new List<LineSegment>();
                segments.Add(new LineSegment(new Point(_model.GetLoad(0) * scale, 0), true));
                segments.Add(new LineSegment(new Point(_model.GetLoad(_model.H) * scale, 500), true));
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
                segments.Add(new LineSegment(new Point(_model.GetLoad(0) * scale, 0), true));
                segments.Add(new LineSegment(new Point(_model.GetLoad(_model.UZ) * scale, WaterLevelPosition), true));
                segments.Add(new LineSegment(new Point(0, WaterLevelPosition), true));
                segments.Add(new LineSegment(new Point(_model.GetLoad(_model.UZ) * scale, WaterLevelPosition), false));
                segments.Add(new LineSegment(new Point(_model.GetLoad(_model.H) * scale, 500), true));
                segments.Add(new LineSegment(new Point(0, 500), true));
                _geometry.Figures.Add(new PathFigure(new Point(0, 0), segments, true));

                return _geometry;
            }
        }
        public double WaterLevelPosition
        {
            get
            {
                return 500 * _model.UZ / _model.H;
            }
        }

        // Converters
        public bool IsWaterAboveBottom
        {
            get
            {
                return _model.UZ < _model.H;
            }
        }

        public bool IsLoadOnTop
        {
            get
            {
                return _model.Q > 0;
            }
        }

    }
}
