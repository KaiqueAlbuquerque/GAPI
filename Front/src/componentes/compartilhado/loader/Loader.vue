<template>
    <div>
        <div class="modal-overlay" id="escurece"></div>
        <div class="preloader-wrapper big active load">
            <div class="spinner-layer spinner-blue-only">
                <div class="circle-clipper left">
                    <div class="circle"></div>
                </div>
                <div class="gap-patch">
                    <div class="circle"></div>
                </div>
                <div class="circle-clipper right">
                    <div class="circle"></div>
                </div>
            </div>
        </div>  
    </div>
</template>

<script>
export default {
    
}
</script>

<style>
#escurece{
    z-index: 1002; 
    display: block; 
    opacity: 0.5;
}
.load{
    position: absolute;
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
    z-index: 2000;
    margin: auto;
}
</style>