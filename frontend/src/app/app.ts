import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { LandslideService } from './services/landslide';
import { error } from 'console';


@Component({
  selector: 'app-root',
  imports: [RouterOutlet, FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('frontend');

  cities: string[] = ['New York', 'Los Angeles', 'Chicago', 'Houston', 'Phoenix'];
  selectedCity: string = '';
 
  result = signal<any>(null)
  loading = signal<boolean>(false);
  errorMessage = signal<string>('');

  constructor(private landslideService: LandslideService) { }
  
  checkLandSlideRisk(): void
  {
    if (!this.selectedCity) {
      this.errorMessage.set('Please select a city.');
      return;
    }

    this.loading.set(true);
    this.errorMessage.set('');
    this.result.set(null);

    this.landslideService.getHotSpot(this.selectedCity).subscribe({
      next: (data) => {
        this.result.set(data);
        this.loading.set(false);
      },
      error: (err) => {
        this.errorMessage.set('Error fetching landslide risk data. Please try again later.');
        console.error('Error fetching landslide risk data:', err);
        this.loading.set(false);
      }
    });
  }
}
