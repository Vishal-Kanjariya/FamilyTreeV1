// Import necessary modules and components
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { GojsAngularModule } from 'gojs-angular';

// Import your components
import { AppComponent } from './app.component';
import { HelloComponent } from './hello.component';

@NgModule({
  // Declare imported modules
  imports: [BrowserModule, FormsModule, GojsAngularModule],
  
  // Declare your components
  declarations: [AppComponent, HelloComponent],
  
  // Specify the bootstrap component
  bootstrap: [AppComponent]
})
export class AppModule { }
