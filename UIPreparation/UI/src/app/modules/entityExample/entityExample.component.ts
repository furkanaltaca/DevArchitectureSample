import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { EntityExample } from './models/EntityExample';
import { EntityExampleService } from './services/EntityExample.service';
import { environment } from 'environments/environment';



declare var jQuery: any;

@Component({
	selector: 'app-entityExample',
	templateUrl: './entityExample.component.html',
	styleUrls: ['./entityExample.component.scss']
})
export class EntityExampleComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','name', 'update','delete'];

	entityExampleList:EntityExample[];
	entityExample:EntityExample=new EntityExample();

	entityExampleAddForm: FormGroup;


	entityExampleId:number;

	constructor(private entityExampleService:EntityExampleService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getEntityExampleList();
    }

	ngOnInit() {

		this.createEntityExampleAddForm();
	}


	getEntityExampleList() {
		this.entityExampleService.getEntityExampleList().subscribe(data => {
			this.entityExampleList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.entityExampleAddForm.valid) {
			this.entityExample = Object.assign({}, this.entityExampleAddForm.value)

			if (this.entityExample.id == 0)
				this.addEntityExample();
			else
				this.updateEntityExample();
		}

	}

	addEntityExample(){

		this.entityExampleService.addEntityExample(this.entityExample).subscribe(data => {
			this.getEntityExampleList();
			this.entityExample = new EntityExample();
			jQuery('#entityexample').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.entityExampleAddForm);

		})

	}

	updateEntityExample(){

		this.entityExampleService.updateEntityExample(this.entityExample).subscribe(data => {

			var index=this.entityExampleList.findIndex(x=>x.id==this.entityExample.id);
			this.entityExampleList[index]=this.entityExample;
			this.dataSource = new MatTableDataSource(this.entityExampleList);
            this.configDataTable();
			this.entityExample = new EntityExample();
			jQuery('#entityexample').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.entityExampleAddForm);

		})

	}

	createEntityExampleAddForm() {
		this.entityExampleAddForm = this.formBuilder.group({		
			id : [0],
name : ["", Validators.required]
		})
	}

	deleteEntityExample(entityExampleId:number){
		this.entityExampleService.deleteEntityExample(entityExampleId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.entityExampleList=this.entityExampleList.filter(x=> x.id!=entityExampleId);
			this.dataSource = new MatTableDataSource(this.entityExampleList);
			this.configDataTable();
		})
	}

	getEntityExampleById(entityExampleId:number){
		this.clearFormGroup(this.entityExampleAddForm);
		this.entityExampleService.getEntityExampleById(entityExampleId).subscribe(data=>{
			this.entityExample=data;
			this.entityExampleAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'id')
				group.get(key).setValue(0);
		});
	}

	checkClaim(claim:string):boolean{
		return this.authService.claimGuard(claim)
	}

	configDataTable(): void {
		this.dataSource.paginator = this.paginator;
		this.dataSource.sort = this.sort;
	}

	applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		this.dataSource.filter = filterValue.trim().toLowerCase();

		if (this.dataSource.paginator) {
			this.dataSource.paginator.firstPage();
		}
	}

  }
