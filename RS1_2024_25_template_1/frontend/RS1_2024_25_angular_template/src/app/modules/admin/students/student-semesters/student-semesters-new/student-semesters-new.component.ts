import {Component, OnInit} from '@angular/core';
import {
  StudentGetByIdEndpointService,
  StudentGetByIdResponse
} from '../../../../../endpoints/student-endpoints/student-get-by-id-endpoint.service';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {YOSGetResponse} from '../student-semesters.component';
import {HttpClient} from '@angular/common/http';
import {ActivatedRoute, Router} from '@angular/router';
import {MySnackbarHelperService} from '../../../../shared/snackbars/my-snackbar-helper.service';
import {Location} from '@angular/common';
import {MyAuthService} from '../../../../../services/auth-services/my-auth.service';
import {setValue} from '@ngx-translate/core';
import {MyConfig} from '../../../../../my-config';

@Component({
  selector: 'app-student-semesters-new',
  standalone: false,

  templateUrl: './student-semesters-new.component.html',
  styleUrl: './student-semesters-new.component.css'
})
export class StudentSemestersNewComponent implements OnInit{

  student: StudentGetByIdResponse | null = null;
  akademskeGodine: AcademicYearResponse[] = [];
  yos: YOSGetResponse[] = [];
  recordedById: number | null = null;


  constructor(
   private http: HttpClient,
   private route: ActivatedRoute,
   private router: Router,
   private fb: FormBuilder,
   private snackBar: MySnackbarHelperService,
   protected location: Location,
   private auth: MyAuthService,
   private studentService: StudentGetByIdEndpointService,
  ) {}

form = new FormGroup({
  datumUpisa: new FormControl('', Validators.required),
  godinaStudija: new FormControl(0,[Validators.required, Validators.min(50), Validators.max(2000)]),
  akademskaGodinaId: new FormControl(0, Validators.required),
  cijenaSkolarine : new FormControl(0),
  obnova: new FormControl(false)
})

  ngOnInit()
  {
    this.loadData();
    this.form.get('obnova')!.disable();
    this.form.get('cijenaSkolarine')!.disable();
    this.form.get('datumUpisa')!
      .setValue(new Date().toISOString().split('T')[0]);

    this.form.get('godinaStudija')!.valueChanges.subscribe( value =>{
      if(Number.isInteger(value))
      {
        let renewal = this.yos.find(val => val.godinaStudija === value)
        != undefined;
        this.form.get('obnova')!.setValue(renewal);
        this.form.get('cijenaSkolarine')!.setValue(renewal ? 400 : 1800);
      }
    })

  }



  loadData()
  {
    this.route.params.subscribe(params => {
      let id = params['id'];
      if(id)
      {
          this.studentService.handleAsync(id).subscribe( studentGet => {
            this.student = studentGet;
          })

        this.http.get<YOSGetResponse[]>(`${MyConfig.api_address}/yos/get/${id}`).subscribe(yos=> {
          this.yos = yos;
          this.form.get('godinaStudija')!.setValue(50);
          console.log(this.yos);
        })

        this.http.get<AcademicYearResponse[]>(`${MyConfig.api_address}/academic-year/get`).subscribe( ay => {
          this.akademskeGodine = ay;
          this.form.get('akademskaGodinaId')!.setValue(this.akademskeGodine[0].id );
        })
      }
    })
    this.recordedById = this.auth.getMyAuthInfo()?.userId ?? null;
  }

  newSemester()
  {
    if(this.form.valid)
    {
      let req = {
        studentId: this.student!.id,
        snimioId: this.recordedById,
        akademskaGodinaId: this.form.get('akademskaGodinaId')!.value,
        godinaStudija: this.form.get('godinaStudija')!.value,
        datumUpisa: this.form.get('datumUpisa')!.value
      }

      this.http.post<number>(`${MyConfig.api_address}/yos/create`, req).subscribe( response =>{
        this.snackBar.showMessage(`uspjesno kreiran semestar id: ${response}`);
      })
    }
  }

}


export interface AcademicYearResponse
{
  id: number;
  name: string;
}
