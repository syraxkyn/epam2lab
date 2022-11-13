using Aircompany.Models;
using Aircompany.Planes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aircompany
{
    public class Airport
    {
        private List<Plane> _planes;
        public Airport(IEnumerable<Plane> planes) => _planes = planes.ToList();
        public List<PassengerPlane> GetPassengersPlanes() =>
            new List<PassengerPlane>((IEnumerable<PassengerPlane>)_planes
            .Where(x => x.GetType() == typeof(PassengerPlane)));

        public List<MilitaryPlane> GetMilitaryPlanes() =>
            new List<MilitaryPlane>((IEnumerable<MilitaryPlane>)_planes
                .Where(x => x.GetType() == typeof(MilitaryPlane)));

        public PassengerPlane GetPassengerPlaneWithMaxPassengersCapacity()
        {
            var passengerPlanes = GetPassengersPlanes();
            var maxCapacity = passengerPlanes.Max(y => y.MaxLoadCapacity);
            return passengerPlanes.FirstOrDefault(x => x.MaxLoadCapacity == maxCapacity);
        }

        public List<MilitaryPlane> GetTransportMilitaryPlanes()
        {
            List<MilitaryPlane> militaryPlanes = GetMilitaryPlanes();
            return militaryPlanes.Where(x => x.GetPlaneType() == MilitaryType.TRANSPORT).ToList(); ;
        }

        public Airport SortByMaxDistance() => new Airport(_planes.OrderBy(w => w.GetMaxFlightDistance()));
        public Airport SortByMaxSpeed() => new Airport(_planes.OrderBy(w => w.GetMaxSpeed()));
        public Airport SortByMaxLoadCapacity() => new Airport(_planes.OrderBy(w => w.GetMaxLoadCapacity()));
        public IEnumerable<Plane> GetPlanes() => _planes;

        public override string ToString()
        {
            var planesString = string.Join(", ", _planes.Select(x => x.GetModel()));
            return ($"Airport{{ planes= {planesString}}}");
        }
    }
}
