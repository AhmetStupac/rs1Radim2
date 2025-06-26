import {Component, OnInit} from '@angular/core';
import {MatTableDataSource} from '@angular/material/table';
import {
  StudentGetByIdEndpointService,
  StudentGetByIdResponse
} from '../../../../endpoints/student-endpoints/student-get-by-id-endpoint.service';
import {HttpClient} from '@angular/common/http';
import {MyConfig} from '../../../../my-config';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-student-semesters',
  standalone: false,

  templateUrl: './student-semesters.component.html',
  styleUrl: './student-semesters.component.css'
})
export class StudentSemestersComponent implements OnInit{

  student: StudentGetByIdResponse | null = null;
  yos: YOSGetResponse[] = [];
  displayedColumns = ['id', 'academicYear', 'year', 'renewal', 'date', 'recordedBy']
  dataSource: MatTableDataSource<YOSGetResponse> = new MatTableDataSource<YOSGetResponse>();

  constructor(
   private studentService: StudentGetByIdEndpointService,
   private http: HttpClient,
   private route: ActivatedRoute,
   private router: Router,
  ) {}

  ngOnInit()
  {
    this.loadData();
  }

  loadData() {

    this.route.params.subscribe(params=>{
      let id = params['id'];
      if(id)
      {
        this.studentService.handleAsync(id).subscribe(studentGet => {
          this.student = studentGet;
        })

        this.http.get<YOSGetResponse[]>(`${MyConfig.api_address}/yos/get/${id}`).subscribe(yos=>{
          this.yos = yos;
          this.dataSource.data = this.yos;
        })
      }
    })
  }


  goToNewSemester()
  {
    this.router.navigate(['/admin/student/semester/new', this.student!.id])
  }

}

export interface YOSGetResponse
{
    id: number;
    obnova: boolean;
    datumUpisa: string;
    godinaStudija: number;
    snimio: string;
    akademskaGodina: string
    akademskaGodinaId: number;
    studentId: number;
}
