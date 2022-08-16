
namespace EarthPressureTest
{
    public class EarthPressureModelTest
    {
        EarthPressureModel model;

        public EarthPressureModelTest()
        {
            model = new EarthPressureModel();
            model.Phi = 37;
            model.GammaPrime = 19;
            model.GammaPrimeU = 12;
            model.UZ = 1.26;
        }

        [Fact]
        public void PhiDConversion()
        {
            Assert.Equal(30.099, model.PhiD, 3);
        }

        [Fact]
        public void LoadAtZero()
        {
            double z = 0;
            Assert.Equal(0, model.getLoadAtRest(z));
            Assert.Equal(0, model.getActiveLoad(z));
        }

        [Fact]
        public void LoadAboveWaterLevel()
        {
            double z = 1.26;
            Assert.Equal(11.934, model.getLoadAtRest(z), 3);
            Assert.Equal(7.948, model.getActiveLoad(z), 3);
        }

        [Fact]
        public void LoadBelowWaterLevel()
        {
            double z = 4.26;
            Assert.Equal(59.88, model.getLoadAtRest(z), 3);
            Assert.Equal(49.9, model.getActiveLoad(z), 3);

        }

        [Fact]
        public void NegativeHeight()
        {
            ArgumentException e = Assert.Throws<ArgumentException>(() => model.getActiveLoad(-1));
            e = Assert.Throws<ArgumentException>(() => model.getLoadAtRest(-1));
        }
    }
}