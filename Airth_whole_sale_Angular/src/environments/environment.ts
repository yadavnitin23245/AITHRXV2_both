// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: true,
    apiUrl: 'https://localhost:7201/api'
   // For Internal Testing
   //  apiUrl: 'https://jdpapitest.aithrx.com/api'

   // For staging 
   // apiUrl: 'http://devaithrwholesaleapi.spadez.co/api'

   // for Client Server
  // apiUrl: 'http://aithrjdpapi.spadez.co/api'

  // For Azure server
 // apiUrl: 'https://airtrapispadez.azurewebsites.net/api'
  
  // For VM
 // apiUrl: 'http://aithrjdpapi.spadez.co/api' 
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
