
import { BaseService } from '../base.service';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { AppSettingsService } from '../../../app-settings.service';
import { OTSummaryReport } from '../../models';
@Injectable({
    providedIn: 'root'
})
export class OTSummaryReportService extends BaseService {
    private otSummaryReportBaseUrl = `${this.Base_API_URL}api/Report/ot-summary-report`;
    constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
        super();
        this.otSummaryReportBaseUrl = `${this.Base_API_URL}api/Report/ot-summary-report`;
    }
    createOTSummaryPdfReport(otSummaryReport: OTSummaryReport) {
        var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
        return this.http.post(`${this.otSummaryReportBaseUrl}-pdf`, otSummaryReport, requestHeader);
    }
}
