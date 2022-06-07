import http from "../http-common";

class CandidateService {
  list() {
    return http.get("/candidate/list");
  }
  get(id) {
    return http.get(`/candidate/get`, { id: id });
  }
  create(data) {
    return http.post("/candidate/add", data);
  }
  update(data) {
    return http.post(`/candidate/update`, data);
  }
  delete(idList) {
    return http.post(`/candidate/delete/`, idList);
  }
  search(data) {
    return http.post(`/candidate/search`, data);
  }
}

export default new CandidateService();