<template>
<div class="d-flex" id="wrapper">
        <!-- Sidebar -->
        <div class="bg-light border-right" id="sidebar-wrapper">
            <div class="list-group list-group-flush">
                <a href="" class="list-group-item list-group-item-action bg-light">Danh sách</a>
                <a href="" class="list-group-item list-group-item-action bg-light">Chưa duyệt</a>
                <a href="" class="list-group-item list-group-item-action bg-light">Liên hệ</a>
                <a href="" class="list-group-item list-group-item-action bg-light">Calendar</a>
            </div>
        </div>

        <div class="container-fluid">
            <CandidateList/>
        </div>
    </div>
</template>

<script>
import CandidateList from './components/CandidateList.vue'

export default {
  name: 'App',
  components: {
    CandidateList
  }
}
</script>

<style>
#wrapper {
    overflow-x: hidden;
}

#sidebar-wrapper {
    min-height: 100vh;
    margin-left: -15rem;
    -webkit-transition: margin .25s ease-out;
    -moz-transition: margin .25s ease-out;
    -o-transition: margin .25s ease-out;
    transition: margin .25s ease-out;
}

    #sidebar-wrapper .sidebar-heading {
        padding: 0.875rem 1.25rem;
        font-size: 1.2rem;
    }

    #sidebar-wrapper .list-group {
        width: 15rem;
    }

#page-content-wrapper {
    min-width: 100vw;
}

#wrapper.toggled #sidebar-wrapper {
    margin-left: 0;
}

@media (min-width: 768px) {
    #sidebar-wrapper {
        margin-left: 0;
    }

    #page-content-wrapper {
        min-width: 0;
        width: 100%;
    }

    #wrapper.toggled #sidebar-wrapper {
        margin-left: -15rem;
    }
}
</style>
