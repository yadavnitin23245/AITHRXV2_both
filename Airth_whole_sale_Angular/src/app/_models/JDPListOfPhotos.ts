export class JDPListOfPhotos {
  id: number;
  jDPVehicleInfoId: number;
  vehicleID: number;
  dealerID: number;
  vehiclePhotoID: string;
  photoUrl: string;
  order: number;
  photoTimestamp: string;
  isActive: boolean | null;
  createdDate: string | null;
  createdBy: number | null;
  updatedDate: string | null;
  updatedBy: number | null;
  deletedDate: string | null;
  deletedBy: number | null;
}