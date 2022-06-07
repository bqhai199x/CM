<template>
  <!-- Button trigger modal -->
<button type="button" class="btn btn-primary mt-3">
    Thêm ứng viên
</button>

<!-- Modal -->
<div class="modal fade" id="myModal1">
    <div class="modal-dialog modal-dialog-centered modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="staticBackdropLabel">Nhập ứng viên</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body" id="myModalBodyDiv1">

            </div>

        </div>
    </div>
</div>

<!-- Modal -->
<div class="modal fade" id="myModal2">
    <div class="modal-dialog modal-dialog-centered modal-lg">
        <div class="modal-content" id="myModalBodyDiv2">
            
        </div>
    </div>
</div>

<input type="hidden" id="hiddenCandidateId" />

    <div class="input-group w-25 py-3">
        <input type="text" class="form-control" placeholder="Tìm kiếm ứng viên" name="namestr" v-model="searchCondition.name">
        <div class="input-group-append">
            <button class="btn btn-primary" type="submit" @click="search">
                Tìm kiếm
            </button>
        </div>
    </div>

<!-- List candidate -->
<table class="table table-bordered">
    <thead>
        <tr class="text-center bg-secondary text-white">
            <th scope="col">Chức danh</th>
            <th scope="col">Vị trí</th>
            <th scope="col">Họ tên</th>
            <th scope="col">Ngày sinh</th>
            <th scope="col">Địa chỉ</th>
            <th scope="col">Số điện thoại</th>
            <th scope="col">Email</th>
            <th scope="col">Người giới thiệu</th>
            <th scope="col">CV</th>
        </tr>
    </thead>
    <tbody>
            <tr v-for="candidate in candidates" :key="candidate.id">
                <td>{{candidate.level.name}}</td>
                <td>{{candidate.position.name}}</td>
                <td>{{candidate.name}}</td>
                <td>{{formatDate(candidate.birthday)}}</td>
                <td>{{candidate.address}}</td>
                <td>{{candidate.phone}}</td>
                <td>{{candidate.email}}</td>
                <td>{{candidate.introduce}}</td>
                <td>
                    <button class="btn btn-link">{{candidate.cvPath}}</button>
                </td>
                <td>
                    <button type="button" class="btn btn-success btn-sm">Sửa</button>&nbsp;
                    <button type="button" class="btn btn-danger btn-sm">Xoá</button>
                </td>
            </tr>
    </tbody>
</table>
</template>

<script>
import candidateService from "../services/candidateService";

export default {
  data(){
    return {
      candidates: [],
      searchCondition: {
          levelid: 0,
          positionid: 0,
          name: "",
          introduce: "",
          status: null
      }
    }
  },
  methods: {
    formatDate(date) {
      const options = { year: 'numeric', month: '2-digit', day: '2-digit' }
      return new Date(date).toLocaleDateString('vi-VN', options)
    },
    search(){
      candidateService.search(this.searchCondition)
      .then(reponse =>{
          this.candidates = reponse.data;
      })
      .catch(e => {
        console.log(e);
      });
    }
  },
  created(){
    candidateService.list()
      .then(reponse =>{
          this.candidates = reponse.data;
      })
      .catch(e => {
        console.log(e);
      });
  },
  watch: {
    searchCondition : {
      handler(){
        this.search();
      },
     deep: true
    }
  }
}
</script>
