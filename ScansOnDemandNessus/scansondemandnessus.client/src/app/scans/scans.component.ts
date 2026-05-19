import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-scans',
  templateUrl: './scans.component.html',
  styleUrls: ['./scans.component.css'],
  standalone: false
})
export class ScansComponent {
  public scans: Scan[] = [];
  public hosts: string[] = [];
  public selectedHost: string = '';

  constructor(
    private http: HttpClient, @Inject('BASE_URL')
    private baseUrl: string,
    private toastr: ToastrService
  ) {
    this.getAllScans();
    this.getAllHosts();
  }

  canCreateScan: boolean = true;
  canDownload: boolean = true;

  private getAllScans() {
    this.http.get<Scan[]>(this.baseUrl + 'nessus/scans').subscribe({
      next: result => this.scans = result,
      error: () => this.toastr.error('Error'),
    });
  }

  private getAllHosts() {
    this.http.get<string[]>(this.baseUrl + 'settings').subscribe({
      next: result => this.hosts = result,
      error: err => this.toastr.error('Error'),
    });
  }

  public createScan() {
    this.canCreateScan = false;

    this.http.post(this.baseUrl + 'nessus/scans', { Name: this.selectedHost }).subscribe({
      error: err => {
        this.toastr.error('Error');
        this.canCreateScan = true;
      },
      complete: () => {
        this.canCreateScan = true; this.getAllScans();
      },
      next: () => this.toastr.success('Scan created', 'Success'),
    });
  }

  public scheduleScan() {
    this.canCreateScan = false;

    this.http.post(this.baseUrl + 'nessus/scans/schedule', { Name: this.selectedHost }).subscribe({
      error: err => {
        this.toastr.error('Error');
        this.canCreateScan = true;
      },
      complete: () => {
        this.canCreateScan = true; this.getAllScans();
      },
      next: () => this.toastr.success('Scan scheduled', 'Success'),
    });
  }

  public runScheduledScans() {
    this.canCreateScan = false;

    this.http.post(this.baseUrl + 'nessus/scans/run', { Name: this.selectedHost }).subscribe({
      error: err => {
        this.toastr.error('Error');
        this.canCreateScan = true;
      },
      complete: () => {
        this.canCreateScan = true; this.getAllScans();
      },
      next: () => this.toastr.success('Scans started', 'Success'),
    });
  }

  public loadScan(scan: Scan) {
    this.canDownload = false;
    this.http.post(this.baseUrl + 'nessus/scans/load', scan).subscribe({
      next: () => this.toastr.success('Scan downloaded', 'Success'),
      error: () => this.toastr.error('Error'),
      complete: () => {
         this.canDownload = true;
      },
    });
  }
}

interface Scan {
  id: number;
  name: string;
  status: string;
}
