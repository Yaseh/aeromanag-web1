#!/usr/bin/env bash
set -euo pipefail

API_URL="http://localhost:5176/api/aeroports"
SWAGGER_URL="http://localhost:5176/swagger"
FRONT_URL="http://localhost:4200"

BACKEND_PID=""

cleanup() {
    if [ -n "$BACKEND_PID" ] && kill -0 "$BACKEND_PID" 2>/dev/null; then
        echo ""
        echo "Arret du backend (PID $BACKEND_PID)..."
        kill "$BACKEND_PID"
        wait "$BACKEND_PID" 2>/dev/null || true
        echo "Backend arrete."
    fi
}
trap cleanup EXIT INT TERM

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$SCRIPT_DIR"

echo ""
echo "=== AeroManag Web — Demarrage ==="
echo ""
echo "Demarrage du backend (API)..."

dotnet run --project src/AeroManag.Api &
BACKEND_PID=$!

echo "Attente que l'API soit prete sur $API_URL ..."
attempt=0
max=60
while [ $attempt -lt $max ]; do
    if ! kill -0 "$BACKEND_PID" 2>/dev/null; then
        echo "ERREUR : le backend s'est arrete de facon inattendue."
        exit 1
    fi
    if curl -sf --max-time 2 "$API_URL" > /dev/null 2>&1; then
        break
    fi
    sleep 1
    attempt=$((attempt + 1))
done

if [ $attempt -ge $max ]; then
    echo "ERREUR : l'API n'a pas repondu apres $max secondes."
    exit 1
fi

echo ""
echo "  API Swagger  : $SWAGGER_URL"
echo "  Frontend     : $FRONT_URL"
echo ""
echo "Appuyez sur Ctrl+C pour tout arreter."
echo ""

cd aeromanag-frontend
ng serve
