document.addEventListener('DOMContentLoaded', function () {
    const canvas = document.getElementById('drawing-board');
    const ctx = canvas.getContext('2d');

    // Set initial background color
    ctx.fillStyle = "#E6C2AF";  // MUST use quotes for hex colors in JS
    ctx.fillRect(0, 0, canvas.width, canvas.height);

    const penTool = document.getElementById('pen-tool');
    const eraserTool = document.getElementById('eraser-tool');
    const colorPicker = document.getElementById('color-picker');
    const brushSize = document.getElementById('brush-size');
    const clearBoard = document.getElementById('clear-board');
    const saveBoard = document.getElementById('save-board');

    // Set canvas size - modified to use fixed size from attributes
    function resizeCanvas() {
        // Keep the fixed size you specified in HTML attributes
        canvas.style.width = '639px';
        canvas.style.height = '572px';
    }

    window.addEventListener('resize', resizeCanvas);
    resizeCanvas();

    // Drawing state
    let isDrawing = false;
    let currentTool = 'pen';
    let currentColor = colorPicker.value;
    let currentSize = brushSize.value;

    // Event listeners for tools
    penTool.addEventListener('click', () => {
        currentTool = 'pen';
        penTool.classList.add('active');
        eraserTool.classList.remove('active');
        ctx.globalCompositeOperation = 'source-over';
    });

    eraserTool.addEventListener('click', () => {
        currentTool = 'eraser';
        eraserTool.classList.add('active');
        penTool.classList.remove('active');
        ctx.globalCompositeOperation = 'destination-out';
    });

    colorPicker.addEventListener('input', () => {
        currentColor = colorPicker.value;
    });

    brushSize.addEventListener('input', () => {
        currentSize = brushSize.value;
    });

    clearBoard.addEventListener('click', () => {
        // Reset to background color when clearing
        ctx.fillStyle = "#E6C2AF";
        ctx.fillRect(0, 0, canvas.width, canvas.height);
    });

    saveBoard.addEventListener('click', () => {
        const dataURL = canvas.toDataURL('image/png');
        const link = document.createElement('a');
        link.download = 'whiteboard-drawing.png';
        link.href = dataURL;
        link.click();
    });

    // Drawing functions
    function startDrawing(e) {
        isDrawing = true;
        draw(e);
    }

    function stopDrawing() {
        isDrawing = false;
        ctx.beginPath();
    }

    function draw(e) {
        if (!isDrawing) return;

        ctx.lineWidth = currentSize;
        ctx.lineCap = 'round';

        if (currentTool === 'pen') {
            ctx.strokeStyle = currentColor;
        } else if (currentTool === 'eraser') {
            ctx.strokeStyle = 'rgba(0,0,0,1)'; // Eraser effect
        }

        // Get position with support for both mouse and touch events
        let x, y;
        if (e.type.includes('touch')) {
            const rect = canvas.getBoundingClientRect();
            x = e.touches[0].clientX - rect.left;
            y = e.touches[0].clientY - rect.top;
        } else {
            x = e.offsetX;
            y = e.offsetY;
        }

        ctx.lineTo(x, y);
        ctx.stroke();
        ctx.beginPath();
        ctx.moveTo(x, y);
    }

    // Mouse events
    canvas.addEventListener('mousedown', startDrawing);
    canvas.addEventListener('mousemove', draw);
    canvas.addEventListener('mouseup', stopDrawing);
    canvas.addEventListener('mouseout', stopDrawing);

    // Touch events for mobile support
    canvas.addEventListener('touchstart', (e) => {
        e.preventDefault();
        startDrawing(e);
    });

    canvas.addEventListener('touchmove', (e) => {
        e.preventDefault();
        draw(e);
    });

    canvas.addEventListener('touchend', (e) => {
        e.preventDefault();
        stopDrawing();
    });

    // Initialize drawing settings
    ctx.strokeStyle = currentColor;
    ctx.lineWidth = currentSize;
    ctx.lineCap = 'round';
});