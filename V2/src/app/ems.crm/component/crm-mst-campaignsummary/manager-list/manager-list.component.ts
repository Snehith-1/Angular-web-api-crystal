import {
	Component, DoCheck, EventEmitter, Input, IterableDiffers, OnChanges,
	Output, SimpleChange
} from '@angular/core';
import { TeamList } from './team-list';
import { ToastrService } from 'ngx-toastr';
import { SocketService } from 'src/app/ems.utilities/services/socket.service';

import { CrmMstCampaignsummaryComponent } from '../crm-mst-campaignsummary.component';


export class ICampaignAssign {
	campaign_gid: string = "";
	campaignassign: string[] = [];
}


export type compareFunction = (a: any, b: any) => number;

var nextId = 0;

@Component({
	selector: 'manager-list',
	styleUrls: ['./manager-list.component.scss'],
	templateUrl: './manager-list.component.html'
})

export class ManagerListComponent implements DoCheck, OnChanges {

	GetUnassignedlength: any;
	responsedata: any;
	CurObj: ICampaignAssign = new ICampaignAssign();
	// CurObj : Productgroup = new Productgroup();
	static AVAILABLE_LIST_NAME = 'available';
	static CONFIRMED_LIST_NAME = 'confirmed';

	static LTR = 'left-to-right';
	static RTL = 'right-to-left';

	static DEFAULT_FORMAT = { add: 'UnAssigned Managers', remove: 'Assigned Managers', all: 'All Select', none: 'All Unselect', direction: ManagerListComponent.LTR, draggable: true };

	@Input() id = `dual-list-${nextId++}`;
	@Input() key2 = '_id';
	@Input() key3 = '_key3';
	@Input() display1 = '_name';
	@Input() height = '100px';
	@Input() filter1 = false;
	@Input() format1 = ManagerListComponent.DEFAULT_FORMAT;
	@Input() sort1 = false;
	@Input()
	compare: compareFunction | undefined;
	@Input() disabled1 = false;
	@Input()
	source1!: Array<any>;
	@Input()
	destination!: Array<any>;
	@Input() presenter: any;
	@Output() destinationChange = new EventEmitter();

	available: TeamList;
	confirmed: TeamList;

	sourceDiffer: any;
	destinationDiffer: any;

	private sorter = (a: any, b: any) => { return (a._name < b._name) ? -1 : ((a._name > b._name) ? 1 : 0); };

	constructor(private differs: IterableDiffers, private ToastrService: ToastrService, public service: SocketService, public getData: CrmMstCampaignsummaryComponent,) {
		this.available = new TeamList(ManagerListComponent.AVAILABLE_LIST_NAME);
		this.confirmed = new TeamList(ManagerListComponent.CONFIRMED_LIST_NAME);
	}

	ngOnChanges(changeRecord: { [key2: string]: SimpleChange }) {
		if (changeRecord['filter1']) {
			if (changeRecord['filter1'].currentValue === false) {
				this.clearFilter(this.available);
				this.clearFilter(this.confirmed);
			}
		}

		if (changeRecord['sort1']) {
			if (changeRecord['sort1'].currentValue === true && this.compare === undefined) {
				this.compare = this.sorter;
			} else if (changeRecord['sort1'].currentValue === false) {
				this.compare = undefined;
			}
		}

		if (changeRecord['format1']) {
			this.format1 = changeRecord['format1'].currentValue;

			if (typeof (this.format1.direction) === 'undefined') {
				this.format1.direction = ManagerListComponent.LTR;
			}

			if (typeof (this.format1.add) === 'undefined') {
				this.format1.add = ManagerListComponent.DEFAULT_FORMAT.add;
			}

			if (typeof (this.format1.remove) === 'undefined') {
				this.format1.remove = ManagerListComponent.DEFAULT_FORMAT.remove;
			}

			if (typeof (this.format1.all) === 'undefined') {
				this.format1.all = ManagerListComponent.DEFAULT_FORMAT.all;
			}

			if (typeof (this.format1.none) === 'undefined') {
				this.format1.none = ManagerListComponent.DEFAULT_FORMAT.none;
			}

			if (typeof (this.format1.draggable) === 'undefined') {
				this.format1.draggable = ManagerListComponent.DEFAULT_FORMAT.draggable;
			}
		}

		if (changeRecord['source']) {
			this.available = new TeamList(ManagerListComponent.AVAILABLE_LIST_NAME);
			this.updatedSource();
			this.updatedDestination();
		}

		if (changeRecord['destination']) {
			this.confirmed = new TeamList(ManagerListComponent.CONFIRMED_LIST_NAME);
			this.updatedDestination();
			this.updatedSource();
		}
	}

	ngDoCheck() {
		if (this.source1 && this.buildAvailable(this.source1)) {
			this.onFilter(this.available);
		}
		if (this.destination && this.buildConfirmed(this.destination)) {
			this.onFilter(this.confirmed);
		}
	}

	buildAvailable(source: Array<any>): boolean {
		const sourceChanges = this.sourceDiffer.diff(source);
		if (sourceChanges) {
			sourceChanges.forEachRemovedItem((r: any) => {
				const idx = this.findItemIndex(this.available.list, r.item, this.key2);
				if (idx !== -1) {
					this.available.list.splice(idx, 1);
				}
			});

			sourceChanges.forEachAddedItem((r: any) => {
				// Do not add duplicates even if source has duplicates.
				if (this.findItemIndex(this.available.list, r.item, this.key2) === -1) {
					this.available.list.push({ _id: this.makeId(r.item), _name: this.makeName(r.item), _key3: this.makeId2(r.item) });
				}
			});

			if (this.compare !== undefined) {
				this.available.list.sort(this.compare);
			}
			this.available.sift = this.available.list;


			return true;
		}
		return false;
	}

	buildConfirmed(destination: Array<any>): boolean {
		let moved = false;
		const destChanges = this.destinationDiffer.diff(destination);
		if (destChanges) {
			destChanges.forEachRemovedItem((r: any) => {
				const idx = this.findItemIndex(this.confirmed.list, r.item, this.key2);
				if (idx !== -1) {
					if (!this.isItemSelected(this.confirmed.pick, this.confirmed.list[idx])) {
						this.selectItem(this.confirmed.pick, this.confirmed.list[idx]);
					}
					this.moveItem(this.confirmed, this.available, this.confirmed.list[idx], false);
					moved = true;

				}
				else {
					if (!this.isItemSelected(this.confirmed.pick, this.confirmed.list[idx])) {
						this.selectItem(this.confirmed.pick, this.confirmed.list[idx]);
					}
					this.moveItem(this.confirmed, this.available, this.confirmed.list[idx], false);
					moved = true;

				}
			});

			destChanges.forEachAddedItem((r: any) => {
				const idx = this.findItemIndex(this.available.list, r.item, this.key2);
				if (idx !== -1) {
					if (!this.isItemSelected(this.available.pick, this.available.list[idx])) {
						this.selectItem(this.available.pick, this.available.list[idx]);
					}
					this.moveItem(this.available, this.confirmed, this.available.list[idx], false);
					moved = true;
				}
				else {

					this.moveItem(this.available, this.confirmed, this.available.list[idx], false);
					moved = true;
					// console.log(this.available.last)
				}
			});

			if (this.compare !== undefined) {
				this.confirmed.list.sort(this.compare);
			}
			this.confirmed.sift = this.confirmed.list;

			if (moved) {
				this.trueUp();
			}
			return true;

		}
		return false;

	}

	updatedSource() {
		this.available.list.length = 0;
		this.available.pick.length = 0;

		if (this.source1 !== undefined) {
			this.sourceDiffer = this.differs.find(this.source1).create();
		}
	}

	updatedDestination() {
		if (this.destination !== undefined) {
			this.destinationDiffer = this.differs.find(this.destination).create();
		}
	}

	direction() {
		return this.format1.direction === ManagerListComponent.LTR;
	}

	dragEnd(list: TeamList) {
		if (list) {
			list.dragStart = false;
		} else {
			this.available.dragStart = false;
			this.confirmed.dragStart = false;
		}
		return false;
	}

	drag(event: DragEvent, item: any, list: TeamList) {
		if (!this.isItemSelected(list.pick, item)) {
			this.selectItem(list.pick, item);
		}
		list.dragStart = true;

		// Set a custom type to be this dual-list's id.
		//event.dataTransfer.setData(this.id, item['_id']);
	}

	allowDrop(event: DragEvent, list: TeamList) {
		if (list.name === 'confirmed') {
			// console.log('confirmed')
			// console.log(list.pick)

			// this.CurObj.productgroup_gid = this.getData.form.value.productgroup_gid;
			// 		this.CurObj.source_list = list.pick;

			// 		this.getData.PostUnmappingvendor(this.CurObj).pipe().subscribe(res=>{

			// 			this.responsedata=res;
			// 			// this.toastr.success('Vendor UnMapping Successfully!');
			// 			// window.location.reload();
			// 			// const dialogConfig = new MatDialogConfig();
			// 			// dialogConfig.disableClose = true;
			// 			// dialogConfig.autoFocus = true;
			// 			// dialogConfig.width = "50%";
			// 			// this.dialog.open(MapcategoryComponent,dialogConfig);

			// 			});

		}
		else {
			// console.log('avaiable')
			// console.log(list.pick)

			// this.CurObj.productgroup_gid = this.getData.form.value.productgroup_gid;
			// 		this.CurObj.source_list = list.pick;
			// 		//console.log(this.CurObj)
			// 		this.getData.PostMappingvendor(this.CurObj).pipe().subscribe(res=>{

			// 			this.responsedata=res;
			// 			// this.toastr.success('Vendor Mapping Successfully!');
			// 			// window.location.reload();
			// 			// this.getData.populateForm();
			// 			// const dialogConfig = new MatDialogConfig();
			// 			// dialogConfig.disableClose = true;
			// 			// dialogConfig.autoFocus = true;
			// 			// dialogConfig.width = "50%";
			// 			// this.dialog.open(MapcategoryComponent,dialogConfig);


			// 			});
		}
		//console.log(list.pick[0])
		//if (event.dataTransfer.types.length && (event.dataTransfer.types[0] === this.id)) {
		event.preventDefault();
		if (!list.dragStart) {
			list.dragOver = true;
		}
	}
	//return false;
	//}

	dragLeave() {
		this.available.dragOver = false;
		this.confirmed.dragOver = false;
	}

	drop(event: DragEvent, list: TeamList) {
		//if (event.dataTransfer.types.length === null && (event.dataTransfer.types[0] === null)) {
		event.preventDefault();
		this.dragLeave();
		this.dragEnd(list);

		if (list === this.available) {
			this.moveItem(this.available, this.confirmed);
		}
		else if (list === this.confirmed) {
			this.moveItem(this.confirmed, this.available);
		}
	}
	//}

	private trueUp() {
		let changed = false;

		// Clear removed items.
		let pos = this.destination.length;
		while ((pos -= 1) >= 0) {
			const mv = this.confirmed.list.filter(conf => {
				if (typeof this.destination[pos] === 'object') {
					return conf._id === this.destination[pos][this.key2];
				} else {
					return conf._id === this.destination[pos];
				}

			});
			if (mv.length === 0) {
				// Not found so remove.
				//console.log(pos)
				this.destination.splice(pos, 1);
				changed = true;
			}
		}


		// Push added items.
		for (let i = 0, len = this.confirmed.list.length; i < len; i += 1) {
			let mv = this.destination.filter((d: any) => {
				if (typeof d === 'object') {
					return (d[this.key2] === this.confirmed.list[i]._id);
				} else {
					return (d === this.confirmed.list[i]._id);
				}
			});

			if (mv.length === 0) {
				// Not found so add.
				mv = this.source1.filter((o: any) => {
					if (typeof o === 'object') {
						return (o[this.key2] === this.confirmed.list[i]._id);
					} else {
						return (o === this.confirmed.list[i]._id);
					}
				});

				if (mv.length > 0) {
					this.destination.push(mv[0]);

					changed = true;
				}

			}


		}

		// console.log(this.confirmed.list)
		if (changed) {
			this.destinationChange.emit(this.destination);
			//console.log(this.destination)
		}
	}

	findItemIndex(list: Array<any>, item: any, key: any = '_id') {
		let idx = -1;

		function matchObject(e: any) {
			if (e._id === item[key]) {
				idx = list.indexOf(e);
				return true;
			}
			return false;
		}

		function match(e: any) {
			if (e._id === item) {
				idx = list.indexOf(e);
				return true;
			}
			return false;
		}

		// Assumption is that the arrays do not have duplicates.
		if (typeof item === 'object') {
			list.filter(matchObject);
			//idx ++
		} else {
			list.filter(match);
		}

		return idx;
	}

	private makeUnavailable(source: TeamList, item: any) {
		const idx = source.list.indexOf(item);
		if (idx !== -1) {
			source.list.splice(idx, 1);
		}
	}
	addbtn(source: TeamList, target: TeamList, item: any = null, trueup = true) {


		this.CurObj.campaignassign = source.pick;

		// let param = {
		// 	campaign_gid: source.pick[0]._key3,
			
		//   }
		// var url = 'CampaignSummary/GetManagerUnassigned'
		  
		// 	  this.service.getparams(url, param).subscribe((result: any) => {
		// 		 this.responsedata=result;
		// 		this.GetUnassignedlength = this.responsedata.GetManagerUnassigned;

		// 	  });
		// 	  const novalue =this.GetUnassignedlength;
		// if ( novalue === undefined) {

			  
		// 	this.ToastrService.warning("Unassign List Available Atleast One Record ! ")

		// }
		// if (this.GetUnassignedlength.length === this.CurObj.campaignassign.length) {

			  
		// 	this.ToastrService.warning("Unassign List Available Atleast One Record ! ")

		// }
		
		// else {
			
			
			if (this.CurObj.campaignassign.length != 0) {

				var url1 = 'CampaignSummary/PostManagerAssignedlist'
				this.service.post(url1, this.CurObj).pipe().subscribe((result: any) => {
	
					if (result.status == false) {
	
	
						this.ToastrService.warning('Error While Manager Assign')
					}
					else {
						this.ToastrService.success('Manager Assigned  Successfully')
						// window.location.reload();
	
					}
	
				});
			}
			else{
				this.ToastrService.warning("Kindly Select Atleast One Record ! ")
			}
		// }
		let i = 0;
		let len = source.pick.length;

		if (item) {
			i = source.list.indexOf(item);
			len = i + 1;
		}

		for (; i < len; i += 1) {
			// Is the pick still in list?
			let mv: Array<any> = [];
			if (item) {
				const idx = this.findItemIndex(source.pick, item);
				if (idx !== -1) {
					mv[0] = source.pick[idx];
				}
				// console.log(item)
			} else {
				mv = source.list.filter(src => {
					//console.log(source.pick)
					return (src._id === source.pick[i]._id);


				});

				// console.log(item)
			}

			// Should only ever be 1
			if (mv.length === 1) {
				// Add if not already in target.
				if (target.list.filter(trg => trg._id === mv[0]._id).length === 0) {
					target.list.push(mv[0]);
					// console.log(target.list)
				}

				this.makeUnavailable(source, mv[0]);
			}

		}

		if (this.compare !== undefined) {
			target.list.sort(this.compare);
		}

		source.pick.length = 0;

		// Update destination
		if (trueup) {
			this.trueUp();

		}

		// Delay ever-so-slightly to prevent race condition.
		setTimeout(() => {
			this.onFilter(source);
			this.onFilter(target);
		}, 10);


	}

	moveItem(source: TeamList, target: TeamList, item: any = null, trueup = true) {
		// console.log(pick)

		let i = 0;
		let len = source.pick.length;

		if (item) {
			i = source.list.indexOf(item);
			len = i + 1;
		}

		for (; i < len; i += 1) {
			// Is the pick still in list?
			let mv: Array<any> = [];
			if (item) {
				const idx = this.findItemIndex(source.pick, item);
				if (idx !== -1) {
					mv[0] = source.pick[idx];
				}
				// console.log(item)
			} else {
				mv = source.list.filter(src => {
					//console.log(source.pick)
					return (src._id === source.pick[i]._id);


				});

				// console.log(item)
			}

			// Should only ever be 1
			if (mv.length === 1) {
				// Add if not already in target.
				if (target.list.filter(trg => trg._id === mv[0]._id).length === 0) {
					target.list.push(mv[0]);
					// console.log(target.list)
				}

				this.makeUnavailable(source, mv[0]);
			}

		}

		if (this.compare !== undefined) {
			target.list.sort(this.compare);
		}

		source.pick.length = 0;

		// Update destination
		if (trueup) {
			this.trueUp();

		}

		// Delay ever-so-slightly to prevent race condition.
		setTimeout(() => {
			this.onFilter(source);
			this.onFilter(target);
		}, 10);
	}
	btnremove(source: TeamList, target: TeamList, item: any = null, trueup = true) {

		this.CurObj.campaignassign = source.pick;
    
		if (this.CurObj.campaignassign.length != 0) {
			var url1 = 'CampaignSummary/PostManagerUnassignedlist'
			this.service.post(url1, this.CurObj).pipe().subscribe((result: any) => {

				if (result.status == false) {


					this.ToastrService.warning('Error While Manager Unassign')
				}
				else {
					this.ToastrService.success('Manager Unassigned Successfully')
        //   window.location.reload();

				}

			});

		}
		else {
			this.ToastrService.warning("Kindly Select Atleast One Record ! ")
		}
		let i = 0;
		let len = source.pick.length;

		if (item) {
			i = source.list.indexOf(item);
			len = i + 1;
		}

		for (; i < len; i += 1) {
			// Is the pick still in list?
			let mv: Array<any> = [];
			if (item) {
				const idx = this.findItemIndex(source.pick, item);
				if (idx !== -1) {
					mv[0] = source.pick[idx];
				}
				// console.log(item)
			} else {
				mv = source.list.filter(src => {
					//console.log(source.pick)
					return (src._id === source.pick[i]._id);


				});

				// console.log(item)
			}

			// Should only ever be 1
			if (mv.length === 1) {
				// Add if not already in target.
				if (target.list.filter(trg => trg._id === mv[0]._id).length === 0) {
					target.list.push(mv[0]);
					// console.log(target.list)
				}

				this.makeUnavailable(source, mv[0]);
			}

		}

		if (this.compare !== undefined) {
			target.list.sort(this.compare);
		}

		source.pick.length = 0;

		// Update destination
		if (trueup) {
			this.trueUp();

		}

		// Delay ever-so-slightly to prevent race condition.
		setTimeout(() => {
			this.onFilter(source);
			this.onFilter(target);
		}, 10);
	}

	isItemSelected(list: Array<any>, item: any) {
		if (list.filter(e => Object.is(e, item)).length > 0) {
			return true;
		}
		return false;
	}

	shiftClick(event: MouseEvent, index: number, source: TeamList, item: any) {
		if (event.shiftKey && source.last && !Object.is(item, source.last)) {
			const idx = source.sift.indexOf(source.last);
			if (index > idx) {
				for (let i = (idx + 1); i < index; i += 1) {
					this.selectItem(source.pick, source.sift[i]);
				}
			} else if (idx !== -1) {
				for (let i = (index + 1); i < idx; i += 1) {
					this.selectItem(source.pick, source.sift[i]);
				}
			}
		}
		source.last = item;

	}

	selectItem(list: Array<any>, item: any) {
		const pk = list.filter((e: any) => {
			return Object.is(e, item);
		});
		if (pk.length > 0) {
			// Already in list, so deselect.
			for (let i = 0, len = pk.length; i < len; i += 1) {
				const idx = list.indexOf(pk[i]);
				if (idx !== -1) {
					list.splice(idx, 1);
				}
			}
		} else {
			list.push(item);

		}
	}

	selectAll(source: TeamList) {
		source.pick.length = 0;
		source.pick = source.sift.slice(0);
	}

	selectNone(source: TeamList) {
		source.pick.length = 0;
	}

	isAllSelected(source: TeamList) {
		if (source.list.length === 0 || source.list.length === source.pick.length) {
			return true;
		}
		return false;
	}

	isAnySelected(source: TeamList) {
		if (source.pick.length > 0) {
			return true;
		}
		return false;
	}

	private unpick(source: TeamList) {
		for (let i = source.pick.length - 1; i >= 0; i -= 1) {
			if (source.sift.indexOf(source.pick[i]) === -1) {
				source.pick.splice(i, 1);
			}
		}
	}

	clearFilter(source: TeamList) {
		if (source) {
			source.picker = '';
			this.onFilter(source);
		}
	}

	onFilter(source: TeamList) {
		if (source.picker.length > 0) {
			const filtered = source.list.filter((item: any) => {
				if (Object.prototype.toString.call(item) === '[object Object]') {
					if (item._name !== undefined) {
						return item._name.toLowerCase().indexOf(source.picker.toLowerCase()) !== -1;
					} else {
						return JSON.stringify(item).toLowerCase().indexOf(source.picker.toLowerCase()) !== -1;
					}
				} else {
					return item.toLowerCase().indexOf(source.picker.toLowerCase()) !== -1;
				}
			});
			source.sift = filtered;
			this.unpick(source);
		} else {
			source.sift = source.list;
		}
	}

	private makeId(item: any): string | number {
		if (typeof item === 'object') {
			return item[this.key2];
		} else {
			return item;
		}
	}
	private makeId2(item: any): string | number {
		if (typeof item === 'object') {
			return item[this.key3];
		} else {
			return item;
		}
	}

	// Allow for complex names by passing an array of strings.
	// Example: [display]="[ '_type.substring(0,1)', '_name' ]"
	private makeName(item: any): string {
		const display = this.display1;

		function fallback(itm: any) {
			switch (Object.prototype.toString.call(itm)) {
				case '[object Number]':
					return itm;
				case '[object String]':
					return itm;
				default:
					if (itm !== undefined) {
						return itm[display];
					} else {
						return 'undefined';
					}
			}
		}

		let str = '';

		if (this.display1 !== undefined) {
			if (Object.prototype.toString.call(this.display1) === '[object Array]') {

				for (let i = 0; i < this.display1.length; i += 1) {
					if (str.length > 0) {
						str = str + '_';
					}

					if (this.display1[i].indexOf('.') === -1) {
						// Simple, just add to string.
						str = str + item[this.display1[i]];

					} else {
						// Complex, some action needs to be performed
						const parts = this.display1[i].split('.');

						const s = item[parts[0]];
						if (s) {
							// Use brute force
							if (parts[1].indexOf('substring') !== -1) {
								const nums = (parts[1].substring(parts[1].indexOf('(') + 1, parts[1].indexOf(')'))).split(',');

								switch (nums.length) {
									case 1:
										str = str + s.substring(parseInt(nums[0], 10));
										break;
									case 2:
										str = str + s.substring(parseInt(nums[0], 10), parseInt(nums[1], 10));
										break;
									default:
										str = str + s;
										break;
								}
							} else {
								// method not approved, so just add s.
								str = str + s;
							}
						}
					}
				}
				return str;
			} else {
				return fallback(item);
			}
		}

		return fallback(item);
	}
}