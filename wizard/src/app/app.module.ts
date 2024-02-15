import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { WizardComponent } from './wizard/wizard.component';

import { WizardStepComponent } from './wizard-step/wizard-step.component';


@NgModule({
  declarations: [
    AppComponent,
    WizardComponent,
    WizardStepComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    
  ],
  providers: [
    provideClientHydration()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
