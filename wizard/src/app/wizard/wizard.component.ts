import { Component, ContentChildren, QueryList, AfterContentInit, Renderer2 } from '@angular/core';
// import { WizardStepComponent } from './wizard-step/wizard-step.component';
// Ensure the file name and casing are correct

import { WizardStepComponent } from './wizard-step/wizard-step.component';






@Component({
  selector: 'app-wizard',
  templateUrl: './wizard.component.html',
  styleUrl: './wizard.component.css'
})
export class WizardComponent implements AfterContentInit {
  @ContentChildren(WizardStepComponent) steps!: QueryList<WizardStepComponent>;
  currentStep = 0;

  constructor(private renderer: Renderer2) {}

  ngAfterContentInit() {
    this.updateStepStatus();
  }

  changeStep(index: number) {
    if (index >= 0 && index < this.steps.length && index <= this.currentStep + 1) {
      this.currentStep = index;
      this.updateStepStatus();
    }
  }

  private updateStepStatus() {
    this.steps.toArray().forEach((step, index) => {
      step.isActive = index === this.currentStep;
    });
  }

  nextStep() {
    if (this.currentStep < this.steps.length - 1) {
      this.steps.toArray()[this.currentStep].isActive = false;
      this.currentStep++;
      this.steps.toArray()[this.currentStep].isActive = true;

      // Enable next step in the navigation
      this.renderer.removeClass(this.steps.toArray()[this.currentStep].elementRef.nativeElement.parentElement, 'disabled');
    }
  }

  previousStep() {
    if (this.currentStep > 0) {
      this.steps.toArray()[this.currentStep].isActive = false;
      this.currentStep--;
      this.steps.toArray()[this.currentStep].isActive = true;
    }
  }
 
}