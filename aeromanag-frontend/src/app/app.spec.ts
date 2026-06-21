import { TestBed } from '@angular/core/testing';
import { provideRouter } from '@angular/router';
import { App } from './app';

describe('App', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [App],
      providers: [provideRouter([])],
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(App);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it('should display 6 navigation links', async () => {
    const fixture = TestBed.createComponent(App);
    fixture.detectChanges();
    await fixture.whenStable();
    const compiled = fixture.nativeElement as HTMLElement;
    const links = compiled.querySelectorAll('nav.navbar a');
    expect(links.length).toBe(6);
    const hrefs = Array.from(links).map(l => l.getAttribute('href'));
    expect(hrefs).toContain('/aeroports');
    expect(hrefs).toContain('/avions');
    expect(hrefs).toContain('/personnels');
    expect(hrefs).toContain('/passagers');
    expect(hrefs).toContain('/vols');
    expect(hrefs).toContain('/reservations');
  });
});
