import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { ModuleWithProviders, NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { MaterialModule, getDutchPaginatorIntl } from "../shared/material/material.module";
import { MatPaginatorIntl } from '@angular/material/paginator';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    MaterialModule
  ],
  entryComponents: [
    // All components about to be loaded "dynamically" need to be declared in the entryComponents section.
  ],
  declarations: [
    // common and shared components/directives/pipes between more than one module and components will be listed here.
  ],
  exports: [
    // common and shared components/directives/pipes between more than one module and components will be listed here.
    CommonModule,
    FormsModule,
    HttpClientModule,
    MaterialModule
  ]
  /* No providers here! Since they’ll be already provided in AppModule. */
})
export class SharedModule {
  static forRoot(): ModuleWithProviders<SharedModule> {
    // Forcing the whole app to use the returned providers from the AppModule only.
    return {
      ngModule: SharedModule,
      providers: [
         /* All of your services here. It will hold the services needed by `itself`. */
         //َAdd By Mehrdad
         { provide: MatPaginatorIntl, useValue: getDutchPaginatorIntl() }
        ]

    };
  }
}
