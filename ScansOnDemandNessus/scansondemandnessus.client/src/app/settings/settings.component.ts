import { Component, Host, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css'],
  standalone: false
})
export class SettingsComponent {
  public plugins: Plugin[] = [];
  public host: string = '';
  public login: string = '';
  public password: string = '';


  constructor(
    private http: HttpClient, @Inject('BASE_URL')
    private baseUrl: string,
    private toastr: ToastrService
  ) {
    this.getAllPlugins();
  }

  public save() {
    console.log(this.host);
    console.log(this.login);
    console.log(this.password);
    console.log(this.plugins.filter(x => x.checked).map(x => x.name));

    const mappedPlugins: { [key: string]: Status } = {};

    this.plugins.forEach(plugin => {
      mappedPlugins[plugin.name] = plugin.checked ? { status: 'enabled' } : { status: "disabled" };
    });

    this.http.post(this.baseUrl + 'settings', {
      Host: this.host,
      Login: this.login,
      Password: this.password,
      Plugins: mappedPlugins
    }).subscribe({
      error: err => this.toastr.error('Error'),
      next: () => this.toastr.success('Settings saved', 'Success'),
    });
  }

  public selectAll() {
    this.plugins.forEach(p => {
      p.checked = true;
    })
  }

  public clearAll() {
    this.plugins.forEach(p => {
      p.checked = false;
    })
  }

  public onPluginSelectionChange(event: any, plugin: Plugin) {
    plugin.checked = event.target.checked;
  }

  private getAllPlugins() {
    this.http.get<string[]>(this.baseUrl + 'nessus/plugins').subscribe({
      next: result => {
        result.forEach(plugin => {
          this.plugins.push({
            name: plugin,
            checked: false
          })
        });
      },
      error: err => this.toastr.error('Error'),
    });
  }
}

interface Plugin {
  name: string,
  checked: boolean;
}

interface Status {
  status: string,
}
