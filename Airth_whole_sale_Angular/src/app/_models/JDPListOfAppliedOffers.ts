export class JDPListOfAppliedOffers {
  id: number;
  jDPVehicleInfoId: number;
  vehicleID: number;
  dealerID: number;
  price: string;
  amount: number | null;
  offerName: string;
  offerDescription: string;
  offerStartDate: string | null;
  offerEndDate: string | null;
  order: number | null;
  isActive: boolean | null;
  createdDate: string | null;
  createdBy: number | null;
  updatedDate: string | null;
  updatedBy: number | null;
  deletedDate: string | null;
  deletedBy: number | null;
}