export class JDPAPICallHistory {
  id: number;
  dealerID: number;
  apiName: string;
  aPICallDetails: string;
  initiatedFromIP: string;
  initiatedTimeStamp: string | null;
  completedTimeStamp: string | null;
  status: string;
  responseFileLocationLink: string;
  pageCount: number | null;
  recordCount: number | null;
  createdDate: string | null;
  createdBy: number | null;
  isArchived: boolean | null;
  archivedDate: string | null;
  archivedBy: number | null;
}
